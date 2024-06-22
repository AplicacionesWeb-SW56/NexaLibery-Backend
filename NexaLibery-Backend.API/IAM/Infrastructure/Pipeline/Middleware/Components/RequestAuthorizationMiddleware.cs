using NexaLibery_Backend.API.IAM.Application.Internal.OutboundServices;
using NexaLibery_Backend.API.IAM.Domain.Model.Queries;
using NexaLibery_Backend.API.IAM.Domain.Services;
using NexaLibery_Backend.API.IAM.Infrastructure.Pipeline.Middleware.Attributes;

namespace NexaLibery_Backend.API.IAM.Infrastructure.Pipeline.Middleware.Components;

public class RequestAuthorizationMiddleware(RequestDelegate next)
{
    /**
     * InvokeAsync is called by the ASP.NET Core runtime.
     * It is used to authorize requests.
     * It validates a token is included in the request header and that the token is valid.
     * If the token is valid then it sets the user in HttpContext.Items["User"].
     */
   public async Task InvokeAsync(HttpContext context, IUserQueryService userQueryService, ITokenService tokenService)
        {
            Console.WriteLine("Entering InvokeAsync");

            // skip authorization if endpoint is decorated with [AllowAnonymous] attribute
            var allowAnonymous = context.GetEndpoint()?.Metadata
                .Any(m => m.GetType() == typeof(AllowAnonymousAttribute));
            Console.WriteLine($"Allow Anonymous is {allowAnonymous}");

            if (allowAnonymous == true)
            {
                Console.WriteLine("Skipping authorization");
                await next(context);
                return;
            }

            Console.WriteLine("Entering authorization");

            // get token from request header
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            // if token is null then return Unauthorized
            if (string.IsNullOrEmpty(token))
            {
                Console.WriteLine("Token is null or empty");
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                return;
            }

            try
            {
                // validate token
                var userId = await tokenService.ValidateToken(token);

                // if token is invalid then return Unauthorized
                if (userId == null)
                {
                    Console.WriteLine("Invalid token");
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    return;
                }

                // get user by id
                var getUserByIdQuery = new GetUserByIdQuery(userId.Value);
                var user = await userQueryService.Handle(getUserByIdQuery);

                // if user not found, return Not Found
                if (user == null)
                {
                    Console.WriteLine("User not found");
                    context.Response.StatusCode = StatusCodes.Status404NotFound;
                    return;
                }

                // set user in HttpContext.Items["User"]
                context.Items["User"] = user;

                Console.WriteLine("Successful authorization. Updating Context...");
                Console.WriteLine("Continuing with Middleware Pipeline");
                await next(context);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            }
        }
}
using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using NexaLibery_Backend.API.IAM.Domain.Services;
using NexaLibery_Backend.API.IAM.Infrastructure.Pipeline.Middleware.Attributes;
using NexaLibery_Backend.API.IAM.Interfaces.REST.Resources;
using NexaLibery_Backend.API.IAM.Interfaces.REST.Transform;

namespace NexaLibery_Backend.API.IAM.Interfaces.REST;

[Authorize]
[ApiController]
[Route("api/v1/authentication")]
[Produces(MediaTypeNames.Application.Json)]
public class AuthenticationController : ControllerBase
{
    private readonly IUserCommandService _userCommandService;

    public AuthenticationController(IUserCommandService userCommandService)
    {
        _userCommandService = userCommandService;
    }

    /**
     * <summary>
     *     Sign in endpoint. It allows to authenticate a user
     * </summary>
     * <param name="signInResource">The sign in resource containing username and password.</param>
     * <returns>The authenticated user resource, including a JWT token</returns>
     */
    [HttpPost("sign-in")]
    [AllowAnonymous]
    public async Task<IActionResult> SignIn([FromBody] SignInResource signInResource)
    {
        try
        {
            var signInCommand = SignInCommandFromResourceAssembler.ToCommandFromResource(
                signInResource
            );
            var authenticatedUser = await _userCommandService.Handle(signInCommand);
            var resource = AuthenticatedUserResourceFromEntityAssembler.ToResourceFromEntity(
                authenticatedUser.user,
                authenticatedUser.token
            );
            return Ok(resource);
        }
        catch (Exception e) {
          return StatusCode(StatusCodes.Status400BadRequest, new { message = e.Message });
        }
    }

    /**
     * <summary>
     *     Sign up endpoint. It allows to create a new user
     * </summary>
     * <param name="signUpResource">The sign up resource containing username and password.</param>
     * <returns>A confirmation message on successful creation.</returns>
     */
    [HttpPost("sign-up")]
    [AllowAnonymous]
    public async Task<IActionResult> SignUp([FromBody] SignUpResource signUpResource)
    {
      try
      {
        var signUpCommand = SignUpCommandFromResourceAssembler.ToCommandFromResource(
            signUpResource
        );
        await _userCommandService.Handle(signUpCommand);
        return Ok(new { message = "User created successfully" });
      }
      catch (Exception e)
      {
        return StatusCode(StatusCodes.Status400BadRequest, new { message = e.Message });
      }
    }
}


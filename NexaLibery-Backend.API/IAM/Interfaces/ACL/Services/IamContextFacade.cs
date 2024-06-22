using NexaLibery_Backend.API.IAM.Domain.Model.Commands;
using NexaLibery_Backend.API.IAM.Domain.Model.Queries;
using NexaLibery_Backend.API.IAM.Domain.Services;


namespace NexaLibery_Backend.API.IAM.Interfaces.ACL.Services;

public class IamContextFacade(IUserCommandService userCommandService, IUserQueryService userQueryService) : IIamContextFacade
{
    public async Task<int> CreateUser(string username, string password, string email, string description, string cardNumber, string bornDate, string photo)
    {
        var signUpCommand = new SignUpCommand(username, password, email, description, cardNumber, bornDate, photo);
        await userCommandService.Handle(signUpCommand);
        var getUserByUsernameQuery = new GetUserByEmailQuery(username);
        var result = await userQueryService.Handle(getUserByUsernameQuery);
        return result?.Id ?? 0;
    }

    public async Task<int> FetchUserIdByUsername(string username)
    {
        var getUserByUsernameQuery = new GetUserByEmailQuery(username);
        var result = await userQueryService.Handle(getUserByUsernameQuery);
        return result?.Id ?? 0;
    }

    public async Task<string> FetchUsernameByUserId(int userId)
    {
        var getUserByIdQuery = new GetUserByIdQuery(userId);
        var result = await userQueryService.Handle(getUserByIdQuery);
        return result?.Username ?? string.Empty;
    }
}
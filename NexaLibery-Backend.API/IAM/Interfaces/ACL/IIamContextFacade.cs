namespace NexaLibery_Backend.API.IAM.Interfaces.ACL;

public interface IIamContextFacade
{
    Task<int> CreateUser(string username, string password, string email, string description, string cardNumber, string bornDate, string photo);
    Task<int> FetchUserIdByUsername(string username);
    Task<string> FetchUsernameByUserId(int userId);
}
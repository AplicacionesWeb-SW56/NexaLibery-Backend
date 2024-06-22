namespace NexaLibery_Backend.API.IAM.Interfaces.REST.Resources;

public record SignUpResource(string Username, string Password,string Email, string Description, string CardNumber,string BornDate,string Photo);
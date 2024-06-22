namespace NexaLibery_Backend.API.IAM.Interfaces.REST.Resources;

public record UserResource(int Id, string Username,string Email, string Description, string CardNumber,string BornDate,string Photo);
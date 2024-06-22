namespace NexaLibery_Backend.API.IAM.Interfaces.REST.Resources;

public record AuthenticatedUserResource(int Id, string Email, string Token);
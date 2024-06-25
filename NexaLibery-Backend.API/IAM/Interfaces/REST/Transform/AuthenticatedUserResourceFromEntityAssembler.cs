using NexaLibery_Backend.API.IAM.Domain.Model.Aggregates;
using NexaLibery_Backend.API.IAM.Interfaces.REST.Resources;

namespace NexaLibery_Backend.API.IAM.Interfaces.REST.Transform;

public static class AuthenticatedUserResourceFromEntityAssembler
{
    public static AuthenticatedUserResource ToResourceFromEntity(
        User user, string token)
    {
        return new AuthenticatedUserResource(user.Id, user.Email, user.Username, token);
    }
}

using NexaLibery_Backend.API.IAM.Domain.Model.Aggregates;
using NexaLibery_Backend.API.IAM.Interfaces.REST.Resources;

namespace NexaLibery_Backend.API.IAM.Interfaces.REST.Transform;

public static class UserResourceFromEntityAssembler
{
    public static UserResource ToResourceFromEntity(User user)
    {
        return new UserResource(user.Id, user.Username,user.Email,user.Description,user.CardNumber,user.BornDate,user.Photo);
    }
}
using NexaLibery_Backend.API.IAM.Domain.Model.Commands;
using NexaLibery_Backend.API.IAM.Interfaces.REST.Resources;

namespace NexaLibery_Backend.API.IAM.Interfaces.REST.Transform;

public static class SignInCommandFromResourceAssembler
{
    public static SignInCommand ToCommandFromResource(SignInResource resource)
    {
        return new SignInCommand(resource.Email, resource.Password);
    }
}
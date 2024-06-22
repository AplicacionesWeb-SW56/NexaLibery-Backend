using NexaLibery_Backend.API.IAM.Domain.Model.Commands;
using NexaLibery_Backend.API.IAM.Interfaces.REST.Resources;


namespace NexaLibery_Backend.API.IAM.Interfaces.REST.Transform;

public static class SignUpCommandFromResourceAssembler
{
    public static SignUpCommand ToCommandFromResource(SignUpResource resource)
    {
        return new SignUpCommand(resource.Username, resource.Password, resource.Email, resource.Description, resource.CardNumber, resource.BornDate, resource.Photo);
    }
}
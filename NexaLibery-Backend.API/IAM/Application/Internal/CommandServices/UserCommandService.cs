using NexaLibery_Backend.API.IAM.Application.Internal.OutboundServices;
using NexaLibery_Backend.API.IAM.Domain.Model.Aggregates;
using NexaLibery_Backend.API.IAM.Domain.Model.Commands;
using NexaLibery_Backend.API.IAM.Domain.Repositories;
using NexaLibery_Backend.API.IAM.Domain.Services;
using NexaLibery_Backend.API.Shared.Domain.Repositories;

namespace NexaLibery_Backend.API.IAM.Application.Internal.CommandServices
{
    public class UserCommandService(
        IUserRepository userRepository,
        ITokenService tokenService,
        IHashingService hashingService,
        IUnitOfWork unitOfWork)
        : IUserCommandService
    {
        /**
         * <summary>
         *     Handle sign in command
         * </summary>
         * <param name="command">The sign in command</param>
         * <returns>The authenticated user and the JWT token</returns>
         */
        public async Task<(User user, string token)> Handle(SignInCommand command)
        {
            var user = await userRepository.FindByEmailAsync(command.Email);

            if (user == null || !hashingService.VerifyPassword(command.Password, user.PasswordHash))
                throw new Exception("Invalid email or password");

            var token = tokenService.GenerateToken(user);

            return (user, token);
        }

        /**
         * <summary>
         *     Handle sign up command
         * </summary>
         * <param name="command">The sign up command</param>
         * <returns>A confirmation message on successful creation.</returns>
         */
        public async Task Handle(SignUpCommand command)
        {
            if (userRepository.ExistsByUsername(command.Email))
                throw new Exception($"Email {command.Email} is already taken");

            var hashedPassword = hashingService.HashPassword(command.Password);
            var user = new User(command.Username, hashedPassword, command.Email, command.Description, command.CardNumber, command.BornDate, command.Photo);
            try
            {
                await userRepository.AddAsync(user);
                await unitOfWork.CompleteAsync();
            }
            catch (Exception e)
            {
                throw new Exception($"An error occurred while creating user: {e.Message}");
            }
        }
    }
}

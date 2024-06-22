using NexaLibery_Backend.API.IAM.Domain.Model.Aggregates;
using NexaLibery_Backend.API.IAM.Domain.Model.Queries;
using NexaLibery_Backend.API.IAM.Domain.Repositories;
using NexaLibery_Backend.API.IAM.Domain.Services;

namespace NexaLibery_Backend.API.IAM.Application.Internal.QueryServices;

public class UserQueryService(IUserRepository userRepository) : IUserQueryService
{
    /**
     * <summary>
     *     Handle get user by id query
     * </summary>
     * <param name="query">The query object containing the user id to search</param>
     * <returns>The user</returns>
     */
    public async Task<User?> Handle(GetUserByIdQuery query)
    {
        return await userRepository.FindByIdAsync(query.Id);
    }

    /**
     * <summary>
     *     Handle get all users query
     * </summary>
     * <param name="query">The query object for getting all users</param>
     * <returns>A list of users</returns>
     */
    public async Task<IEnumerable<User>> Handle(GetAllUsersQuery query)
    {
        return await userRepository.ListAsync();
    }

    /**
     * <summary>
     *     Handle get user by email query
     * </summary>
     * <param name="query">The query object containing the email to search</param>
     * <returns>The user</returns>
     */
    public async Task<User?> Handle(GetUserByEmailQuery query)
    {
        return await userRepository.FindByEmailAsync(query.Email);
    }

 
}
using NexaLibery_Backend.API.IAM.Domain.Model.Aggregates;
using NexaLibery_Backend.API.Shared.Domain.Repositories;

namespace NexaLibery_Backend.API.IAM.Domain.Repositories;

/**
 * <summary>
 *     The user repository
 * </summary>
 * <remarks>
 *     This repository is used to manage users
 * </remarks>
 */
public interface IUserRepository : IBaseRepository<User>
{
    Task<User?> FindByIdAsync(int id);
    Task<User?> FindByEmailAsync(string email); 
    
    bool ExistsByUsername(string username);
    
    /**
    * <summary>
    *     Find a user by id
    * </summary>
    * <param name="username">The username to search</param>
    * <returns>The user</returns>
    */
    Task<User?> FindByUsernameAsync(string username);
}
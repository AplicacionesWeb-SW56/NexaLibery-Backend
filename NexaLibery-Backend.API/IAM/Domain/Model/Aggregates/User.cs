using System.Text.Json.Serialization;

namespace NexaLibery_Backend.API.IAM.Domain.Model.Aggregates;

public class User(string username, string passwordHash,string email,string description,string cardNumber,string bornDate,string photo)
{
    public User() : this(string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty)
    {
    }

    public int Id { get; }
    public string Username { get; private set; } = username;

    [JsonIgnore] public string PasswordHash { get; private set; } = passwordHash;
    
    public string Email { get; private set; } = email;
    public string Description { get; private set; } = description;
    public string CardNumber { get; private set; } = cardNumber;
    public string BornDate { get; private set; } = bornDate;
    public string Photo { get; private set; } = photo;
    
    /**
     * <summary>
     *     Update the username
     * </summary>
     * <param name="username">The new username</param>
     * <returns>The updated user</returns>
     */
    public User UpdateUsername(string username)
    {
        Username = username;
        return this;
    }

    /**
     * <summary>
     *     Update the password hash
     * </summary>
     * <param name="passwordHash">The new password hash</param>
     * <returns>The updated user</returns>
     */
    public User UpdatePasswordHash(string passwordHash)
    {
        PasswordHash = passwordHash;
        return this;
    }
}
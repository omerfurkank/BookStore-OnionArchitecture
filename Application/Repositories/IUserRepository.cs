using Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;

namespace Application.Repositories;

public interface IUserRepository
{
    public Task<User?> GetUserByEmailAsync(string email);
    public Task<User?> GetUserByIdAsync(int id);
    public Task<IList<string>> GetUserRolesAsync(User user);
    public Task<IList<User>> GetAllUserAsync();
    public Task<IdentityResult> AddRolesToUserAsync(int id, string[] roles);
    public Task<IdentityResult> CreateUserAsync(User user,string password);
    public Task<IdentityResult> UpdateUserAsync(User user);
    public Task<IdentityResult> SetAccessTokenAsync(User user, string token);
}

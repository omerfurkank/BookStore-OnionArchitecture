using Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;

namespace Application.Repositories;

public interface IUserRepository
{
    public Task<User?> GetUserAsync(string email);
    public Task<IList<string>> GetUserRolesAsync(User user);
    public Task<IdentityResult> AddRoleToUserAsync(User user, string role);
    public Task<IdentityResult> CreateUserAsync(User user,string password);
    public Task<IdentityResult> UpdateUserAsync(User user);
    public Task<IdentityResult> SetAccessTokenAsync(User user, string token);
}

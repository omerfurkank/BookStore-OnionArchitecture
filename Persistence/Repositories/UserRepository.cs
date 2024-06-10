using Application.Repositories;
using Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories;
public class UserRepository : IUserRepository
{
    private readonly UserManager<User> _userManager;

    public UserRepository(UserManager<User> userManager)
    {
        _userManager = userManager;
    }
    public async Task<User?> GetUserAsync(string email) => await _userManager.FindByEmailAsync(email);
    public async Task<IList<string>> GetUserRolesAsync(User user) => await _userManager.GetRolesAsync(user);
    public async Task<IdentityResult> AddRoleToUserAsync(User user, string role) => await _userManager.AddToRoleAsync(user, role);

    public async Task<IdentityResult> CreateUserAsync(User user, string password) => await _userManager.CreateAsync(user,password);
    public async Task<IdentityResult> UpdateUserAsync(User user) => await _userManager.UpdateAsync(user);
    public async Task<IdentityResult> SetAccessTokenAsync(User user, string token) => await _userManager.SetAuthenticationTokenAsync(user, "Default", "AccessToken", token);

}

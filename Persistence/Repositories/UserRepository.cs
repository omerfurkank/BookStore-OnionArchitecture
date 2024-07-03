using Application.Repositories;
using Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories;
public class UserRepository : IUserRepository
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;

    public UserRepository(UserManager<User> userManager, SignInManager<User> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }
    public async Task Logout() => await _signInManager.SignOutAsync();
    public async Task<User?> GetUserByEmailAsync(string email) => await _userManager.FindByEmailAsync(email);
    public async Task<User?> GetUserByIdAsync(string id) => await _userManager.FindByIdAsync(id);
    public async Task<IList<User>> GetAllUserAsync() => await _userManager.Users.ToListAsync();
    public async Task<IList<string>> GetUserRolesAsync(User user) => await _userManager.GetRolesAsync(user);
    public async Task<IdentityResult> AddRolesToUserAsync(int id, string[] addedRoles)
    {      
           User? user = await _userManager.FindByIdAsync(id.ToString());
           return await _userManager.AddToRolesAsync(user, addedRoles);       
    } 

    public async Task<IdentityResult> CreateUserAsync(User user, string password) => await _userManager.CreateAsync(user,password);
    public async Task<IdentityResult> UpdateUserAsync(User user) => await _userManager.UpdateAsync(user);
    public async Task<IdentityResult> SetAccessTokenAsync(User user, string token) => await _userManager.SetAuthenticationTokenAsync(user, "Default", "AccessToken", token);

}

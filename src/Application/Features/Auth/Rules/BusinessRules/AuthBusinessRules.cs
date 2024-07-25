using Application.Exceptions.CustomExceptions;
using Application.Features.Books.Constants;
using Application.Repositories;
using Domain.Entities;
using Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Auth.Rules.BusinessRules;
public class AuthBusinessRules
{
    private readonly UserManager<User> _userManager;

    public AuthBusinessRules(UserManager<User> userManager)
    {
        _userManager = userManager;
    }

    public async Task CheckUserExistsToRegister(string email)
    {
        var result = await _userManager.FindByEmailAsync(email);

        if (result is not null) { throw new AuthorizationException("AuthMessages.UserExists"); }
    }
    public async Task CheckUserExistsToLogin(string email)
    {
        var result = await _userManager.FindByEmailAsync(email);

        if (result is null) { throw new AuthorizationException("AuthMessages.UserDoesNotExist"); }
    }
    public async Task CheckPasswordToLogin(User user, string password)
    {
        bool result = await _userManager.CheckPasswordAsync(user, password);

        if (!result) { throw new AuthorizationException("AuthMessages.PasswordDoesNotMach"); }
    }
    //public void CheckPasswordToRequiredParameters(string password)
    //{
    //    var options = new PasswordOptions
    //    {
    //        RequireDigit = true,
    //        RequiredLength = 6,
    //        RequireNonAlphanumeric = true,
    //        RequireUppercase = true,
    //        RequireLowercase = true
    //    };

    //    if (password.Length < options.RequiredLength)
    //        throw new AuthorizationException($"Password must be at least {options.RequiredLength} characters long.");

    //    if (options.RequireDigit && !password.Any(char.IsDigit))
    //        throw new AuthorizationException("Password must contain at least one digit.");

    //    if (options.RequireLowercase && !password.Any(char.IsLower))
    //        throw new AuthorizationException("Password must contain at least one lowercase letter.");

    //    if (options.RequireUppercase && !password.Any(char.IsUpper))
    //        throw new AuthorizationException("Password must contain at least one uppercase letter.");

    //    if (options.RequireNonAlphanumeric && password.All(char.IsLetterOrDigit))
    //        throw new AuthorizationException("Password must contain at least one non-alphanumeric character.");
    //}
    public void CheckRefreshTokenExpiredDate(DateTime? expiredDate)
    {
        if (expiredDate is null || expiredDate <= DateTime.UtcNow) throw new BusinessException("AuthMessages.RefreshTokenisExpired");
    }
    public void CheckRefreshTokenIsNull(User user)
    {
        if (user.RefreshToken is null) throw new AuthorizationException("AuthMessages.RefreshTokenDoesNotExist");
    }
}

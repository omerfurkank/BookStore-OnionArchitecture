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

        if (result is not null) { throw new BusinessException("AuthMessages.UserExists"); }
    }
    public async Task CheckUserExistsToLogin(string email)
    {
        var result = await _userManager.FindByEmailAsync(email);

        if (result is null) { throw new BusinessException("AuthMessages.UserDoesNotExist"); }
    }
    public async Task CheckPasswordToLogin(User user, string password)
    {
        bool result = await _userManager.CheckPasswordAsync(user, password);

        if (!result) { throw new BusinessException("AuthMessages.PasswordDoesNotMach"); }
    }
    public void CheckRefreshTokenExpiredDate(DateTime? expiredDate)
    {
        if (expiredDate is null || expiredDate <= DateTime.UtcNow) throw new BusinessException("AuthMessages.RefreshTokenisExpired");
    }
    public void CheckRefreshTokenIsNull(User user)
    {
        if (user.RefreshToken is null) throw new BusinessException("AuthMessages.RefreshTokenDoesNotExist");
    }
}

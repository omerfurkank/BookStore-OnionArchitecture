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

    public async Task CheckUserExists(string email)
    {
        var result = await _userManager.FindByEmailAsync(email);

        if (result is not null) { throw new BusinessException("AuthMessages.UserExists"); }
    }
}

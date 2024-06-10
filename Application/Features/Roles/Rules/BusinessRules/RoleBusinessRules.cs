using Application.Exceptions.CustomExceptions;
using Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Roles.Rules.BusinessRules;
public class RoleBusinessRules
{
    private readonly RoleManager<Role> _roleManager;

    public RoleBusinessRules(RoleManager<Role> roleManager)
    {
        _roleManager = roleManager;
    }
    public async Task CheckRoleExists(string roleName)
    {
        var result = await _roleManager.RoleExistsAsync(roleName);
        if (result) { throw new BusinessException(); }
    }
}

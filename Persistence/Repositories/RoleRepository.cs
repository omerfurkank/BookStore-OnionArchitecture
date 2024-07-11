using Application.Repositories;
using Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories;
public class RoleRepository : IRoleRepository
{
    private readonly RoleManager<Role> _roleManager;
    public RoleRepository(RoleManager<Role> roleManager)
    {
       _roleManager = roleManager;
    }
    public async Task<IdentityResult> CreateRole(string name)
    {
        IdentityResult result = await _roleManager.CreateAsync(new() {Name = name });
        return result;      
    }
    public async Task<bool> DeleteRole(int id)
    {
        Role? role = await _roleManager.FindByIdAsync(id.ToString());
        IdentityResult result = await _roleManager.DeleteAsync(role);
        return result.Succeeded;
    }
    public IList<string> GetList()
    {
        IList<string>? response = _roleManager.Roles.Select(r => r.Name).ToList();
        return response;
    }
    //public (object, int) GetAllRoles(int index, int size)
    //{
    //    var query = _roleManager.Roles;

    //    IQueryable<Role> rolesQuery = null;

    //    if (index != -1 && size != -1)
    //        rolesQuery = query.Skip(index * size).Take(size);
    //    else
    //        rolesQuery = query;

    //    return (rolesQuery.Select(r => new { r.Id, r.Name }), query.Count());
    //}

    public async Task<(int id, string name)> GetByIdRole(int id)
    {
        string role = await _roleManager.GetRoleIdAsync(new() { Id = id });
        return (id, role);
    }

    public async Task<bool> UpdateRole(int id, string name)
    {
        Role role = await _roleManager.FindByIdAsync(id.ToString());
        role.Name = name;
        IdentityResult result = await _roleManager.UpdateAsync(role);
        return result.Succeeded;
    }
}


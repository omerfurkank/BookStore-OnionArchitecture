using Application.Features.Roles.Rules.BusinessRules;
using AutoMapper;
using Domain.Entities.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Roles.Commands.CreateRole;
public class CreateRoleCommandHandler : IRequestHandler<CreateRoleCommandRequest, CreateRoleCommandResponse>
{
    private readonly RoleManager<Role> _roleManager;
    private readonly IMapper _mapper;
    private readonly RoleBusinessRules _roleBusinessRules;

    public CreateRoleCommandHandler(RoleManager<Role> roleManager, IMapper mapper, RoleBusinessRules roleBusinessRules)
    {
        _roleManager = roleManager;
        _mapper = mapper;
        _roleBusinessRules = roleBusinessRules;
    }

    public async Task<CreateRoleCommandResponse> Handle(CreateRoleCommandRequest request, CancellationToken cancellationToken)
    {
        await _roleBusinessRules.CheckRoleExists(request.Name);

        var role = _mapper.Map<Role>(request);
        var result = await _roleManager.CreateAsync(role);
        if (!result.Succeeded)
        {
            var response = _mapper.Map<CreateRoleCommandResponse>(result);
            return response;
        }
        throw new Exception("RoleMessages.RoleExists");

    }
}

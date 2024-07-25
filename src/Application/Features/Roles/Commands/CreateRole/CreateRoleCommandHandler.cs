using Application.Features.Roles.Rules.BusinessRules;
using Application.Repositories;
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
    private readonly IRoleRepository _roleRepository;
    private readonly IMapper _mapper;
    private readonly RoleBusinessRules _roleBusinessRules;

    public CreateRoleCommandHandler(IRoleRepository roleRepository, IMapper mapper, RoleBusinessRules roleBusinessRules)
    {
        _roleRepository = roleRepository;
        _mapper = mapper;
        _roleBusinessRules = roleBusinessRules;
    }

    public async Task<CreateRoleCommandResponse> Handle(CreateRoleCommandRequest request, CancellationToken cancellationToken)
    {
        await _roleBusinessRules.CheckRoleExists(request.Name);

        var result = await _roleRepository.CreateRole(request.Name);
        if (result.Succeeded)
        {
            CreateRoleCommandResponse response = new() { Name = request.Name };
            return response;
        }
        
        throw new Exception("RoleMessages.RoleExists");

    }
}

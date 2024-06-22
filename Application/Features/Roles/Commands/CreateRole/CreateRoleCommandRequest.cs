using Application.Pipelines.Auth;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Roles.Commands.CreateRole;
public class CreateRoleCommandRequest : IRequest<CreateRoleCommandResponse>, ISecuredRequest
{
    public string Name { get; set; }
    public string[] Roles => new[] {"admin"};
}

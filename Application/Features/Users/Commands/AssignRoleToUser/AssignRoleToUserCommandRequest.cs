using Application.Pipelines.Auth;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Users.Commands.AssignRoleToUser;
public class AssignRoleToUserCommandRequest : IRequest<AssignRoleToUserCommandResponse>, ISecuredRequest
{
    public int UserId { get; set; }
    public string[]? RolesToBeAdded { get; set; }
    public string[] Roles => new[] { "admin" };
}

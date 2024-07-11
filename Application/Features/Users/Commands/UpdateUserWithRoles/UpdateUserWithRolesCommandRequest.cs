using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Users.Commands.UpdateUserWithRoles;
public class UpdateUserWithRolesCommandRequest : IRequest<UpdateUserWithRolesCommandResponse>
{
    public int Id { get; set; }
    public string? FullName { get; set; }
    public string? Email { get; set; }
    public string[]? Roles { get; set; }
}

using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Auth.Commands.Logout;
public class LogoutCommandRequest : IRequest<LogoutCommandResponse>
{
    public string? Email { get; set; }
}

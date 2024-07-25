using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Auth.Commands.Login;
public class LoginCommandRequest : IRequest<LoginCommandResponse>
{
    [DefaultValue("frkn@g.com")]
    public string? Email { get; set; }
    [DefaultValue("123Asd,")]
    public string? Password { get; set; }
}

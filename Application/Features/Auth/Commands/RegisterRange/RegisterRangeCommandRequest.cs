using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Auth.Commands.RegisterRange;
public class RegisterRangeCommandRequest : IRequest<RegisterRangeCommandResponse>
{
    public List<RegisterRequestDto> Users { get; set; }
    public class RegisterRequestDto
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PasswordConfirm { get; set; }
    }
}


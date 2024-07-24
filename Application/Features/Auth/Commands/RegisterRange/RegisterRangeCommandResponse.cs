using Domain.Entities.Identity;

namespace Application.Features.Auth.Commands.RegisterRange;

public class RegisterRangeCommandResponse
{
    public List<RegisterResponseDto> Users { get; set; }
    public class RegisterResponseDto
    {
        public string FullName { get; set; }
        public string Email { get; set; }
    }
}


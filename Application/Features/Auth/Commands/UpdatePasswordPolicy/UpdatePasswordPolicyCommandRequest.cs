using MediatR;

namespace Application.Features.Auth.Commands.UpdatePasswordPolicy;

public class UpdatePasswordPolicyCommandRequest : IRequest<UpdatePasswordPolicyCommandResponse>
{
    public bool RequireDigit { get; set; }
    public bool RequireLowerCase { get; set; }
    public bool RequireUpperCase { get; set; }
    public bool RequireNonAlphanumeric { get; set; }
    public int RequiredLength { get; set; }
}

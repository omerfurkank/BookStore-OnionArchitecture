namespace Application.Features.Auth.Commands.Register;

public class RegisterCommandResponse
{
    public string FullName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public bool Succeeded { get; set; }
}

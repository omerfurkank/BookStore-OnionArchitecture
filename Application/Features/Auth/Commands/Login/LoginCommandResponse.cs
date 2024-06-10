namespace Application.Features.Auth.Commands.Login;

public class LoginCommandResponse
{
    public string AccessToken { get; set; }
    public string RefreshToken { get; set; }
    public DateTime AccessTokenExpiredTime { get; set; }
}

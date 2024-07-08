namespace WebApp.Models.Auth;

public class LoginResponseModel
{
    public string AccessToken { get; set; }
    public string RefreshToken { get; set; }
    public DateTime AccessTokenExpiredTime { get; set; }
    public DateTime RefreshTokenExpiredTime { get; set; }
}

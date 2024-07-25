using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using WebApp.Models.Auth;

namespace WebApp.Middlewares;

public class RefreshTokenMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IConfiguration _configuration;
    private readonly IHttpClientFactory _clientFactory;

    public RefreshTokenMiddleware(RequestDelegate next, IConfiguration configuration, IHttpClientFactory clientFactory)
    {
        _next = next;
        _configuration = configuration;
        _clientFactory = clientFactory;
    }

    public async Task InvokeAsync(HttpContext context)
    {

        var accessToken = context.User?.Claims?.FirstOrDefault(x => x.Type == "accessToken")?.Value;
        var refreshTokenExpire = context.User?.Claims?.FirstOrDefault(x => x.Type == "refreshTokenExpire")?.Value;

        if (!string.IsNullOrEmpty(accessToken))
        {
            var handler = new JwtSecurityTokenHandler();
            var accessTokenValid = handler.ReadJwtToken(accessToken);

            if (string.IsNullOrEmpty(refreshTokenExpire) || accessTokenValid.ValidTo < DateTime.UtcNow && DateTime.Parse(refreshTokenExpire) < DateTime.UtcNow)
            {
                await context.SignOutAsync(JwtBearerDefaults.AuthenticationScheme);
                context.Response.Redirect("/Auth/Login");
                return;
            }

            if (accessTokenValid.ValidTo < DateTime.UtcNow)
            {
                var refreshLoginModel = new RefreshLoginModel
                {
                    AccessToken = accessToken
                };

                var client = _clientFactory.CreateClient();
                var content = new StringContent(JsonSerializer.Serialize(refreshLoginModel), Encoding.UTF8, "application/json");
                var response = await client.PostAsync("http://localhost:5298/api/Auth/RefreshTokenLogin", content);

                if (!response.IsSuccessStatusCode)
                {
                    await context.SignOutAsync(JwtBearerDefaults.AuthenticationScheme);
                    context.Response.Redirect("/Auth/Login");
                    return;
                }

                var jsonData = await response.Content.ReadAsStringAsync();
                var newToken = JsonSerializer.Deserialize<RefreshLoginResponseModel>(jsonData, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });

                var claims = accessTokenValid.Claims.ToList();
                claims.Add(new Claim("accessToken", newToken.AccessToken));
                claims.Add(new Claim("refreshTokenExpire", refreshTokenExpire.ToString()));
                var claimsIdentity = new ClaimsIdentity(claims, JwtBearerDefaults.AuthenticationScheme);

                await context.SignInAsync(JwtBearerDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
            }
        }
        else
        {

            if (!context.Request.Path.Value.StartsWith("/Auth/"))
            {
                context.Response.Redirect("/Auth/Login");
                return;
            }    
        }
        await _next(context);
    }
}

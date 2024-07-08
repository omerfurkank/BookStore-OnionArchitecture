using Domain.Entities.Identity;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using NuGet.Common;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json;

namespace WebApp.Middlewares;

public class JwtMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IConfiguration _configuration;
    private readonly IHttpClientFactory _clientFactory;
    public JwtMiddleware(RequestDelegate next, IConfiguration configuration, IHttpClientFactory clientFactory)
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
            var claims = accessTokenValid.Claims.ToList();

            if (accessTokenValid.ValidTo < DateTime.UtcNow /*&& DateTime.Parse(refreshTokenExpire) < DateTime.UtcNow*/)
            {
                await context.SignOutAsync(JwtBearerDefaults.AuthenticationScheme);
                context.Response.Redirect("/Auth/Login");
                return;
            }
            //if (accessTokenValid.ValidTo < DateTime.UtcNow)
            //{
            //    var client = _clientFactory.CreateClient();
            //    var content = new StringContent(JsonSerializer.Serialize(accessToken), Encoding.UTF8, "application/json");
            //    var response = await client.PostAsync("http://localhost:5298/api/Auth/RefreshToken", content);
            //    var jsonData = await response.Content.ReadAsStringAsync();
            //    string newToken = JsonSerializer.Deserialize<string>(jsonData, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
            //    claims.Remove(claims.FirstOrDefault(x => x.Type == "accessToken"));
            //    claims.Add(new Claim("accessToken", newToken));
            //}
        }
        else
        {
            if (!context.Request.Path.Value.StartsWith("/Auth"))
            {
                context.Response.Redirect("/Auth/Login");
                return;
            }
        }

        await _next(context);
    }
}

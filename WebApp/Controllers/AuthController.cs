using Microsoft.AspNetCore.Mvc;
using System.Reflection;
using System.Text.Json;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using WebApp.Models.Auth;

namespace WebApp.Controllers;
public class AuthController : Controller
{
    private readonly IHttpClientFactory _httpClientFactory;

    public AuthController(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public IActionResult Login()
    {
        return View(new LoginModel());
    }
    [HttpPost]
    public async Task<IActionResult> Login(LoginModel userLoginModel)
    {
        var client = _httpClientFactory.CreateClient();
        var content = new StringContent(JsonSerializer.Serialize(userLoginModel), Encoding.UTF8, "application/json");
        var response = await client.PostAsync("http://localhost:5298/api/Auth/Login", content);

        var jsonData = await response.Content.ReadAsStringAsync();
        var tokenModel = JsonSerializer.Deserialize<LoginResponseModel>(jsonData, new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        });

        JwtSecurityTokenHandler handler = new();
        var token = handler.ReadJwtToken(tokenModel.AccessToken);
        var claims = token.Claims.ToList();
        claims.Add(new Claim("accessToken", tokenModel.AccessToken));
        var claimsIdentity = new ClaimsIdentity(claims, JwtBearerDefaults.AuthenticationScheme);
        var authProps = new AuthenticationProperties
        {
            ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(15),
            IsPersistent = true,
        };
        await HttpContext.SignInAsync(JwtBearerDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProps);
        return RedirectToAction("Index", "Home");
    }
    //public Task<IActionResult> Register()
    //{
    //    return View();
    //}
}

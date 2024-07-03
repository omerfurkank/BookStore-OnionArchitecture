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
    private readonly IHttpClientFactory _clientFactory;
    private readonly HttpClient _client;
    public AuthController(IHttpClientFactory httpClientFactory, HttpClient client)
    {
        _clientFactory = httpClientFactory;
        _client = _clientFactory.CreateClient();
    }
    private void Connect()
    {
        var token = User?.Claims?.FirstOrDefault(x => x.Type == "accessToken")?.Value;
        _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
    }
    public IActionResult Login()
    {
        return View(new LoginModel());
    }
    [HttpPost]
    public async Task<IActionResult> Login(LoginModel userLoginModel)
    {
        var client = _clientFactory.CreateClient();
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
    public IActionResult Register()
    {
        return View(new RegisterModel());
    }
    [HttpPost]
    public async Task<IActionResult> Register(RegisterModel registerModel)
    {
        var client = _clientFactory.CreateClient();
        var content = new StringContent(JsonSerializer.Serialize(registerModel), Encoding.UTF8, "application/json");
        var response = await client.PostAsync("http://localhost:5298/api/Auth/Register", content);
        return RedirectToAction("Index", "Home");
    }
    [HttpPost]
    public async Task<IActionResult> Logout()
    {
        Connect();
        var client = _clientFactory.CreateClient();

        var data= User.Claims.FirstOrDefault(x=>x.Type==JwtRegisteredClaimNames.Email);
        var email = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email);
        var model = new LogoutModel() { Email = User.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Email).Value };
        var content = new StringContent(JsonSerializer.Serialize(model), Encoding.UTF8, "application/json");
        var response = await client.PostAsync("http://localhost:5298/api/Auth/Logout", content);
        return RedirectToAction("Index", "Home");
    }
}

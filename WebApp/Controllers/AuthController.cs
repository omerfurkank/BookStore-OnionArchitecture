using Microsoft.AspNetCore.Mvc;
using System.Reflection;
using System.Text.Json;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using WebApp.Models.Auth;
using ClosedXML.Excel;

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
        var tokenModel = JsonSerializer.Deserialize<LoginResponseModel>(jsonData, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase});

        JwtSecurityTokenHandler handler = new();
        var token = handler.ReadJwtToken(tokenModel.AccessToken);
        var claims = token.Claims.ToList();
        claims.Add(new Claim("accessToken", tokenModel.AccessToken));
        claims.Add(new Claim("refreshTokenExpire", tokenModel.RefreshTokenExpiredTime.ToString()));
        var claimsIdentity = new ClaimsIdentity(claims, JwtBearerDefaults.AuthenticationScheme);

        await HttpContext.SignInAsync(JwtBearerDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
        return RedirectToAction("Index", "Home");
    }
    public IActionResult Register()
    {
        return View();
    }
  
    public async Task<IActionResult> Logout()
    {
        var client = _clientFactory.CreateClient();
        string email = User.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Email).Value;
        var model = new LogoutModel() { Email = email };
        var content = new StringContent(JsonSerializer.Serialize(model), Encoding.UTF8, "application/json");
        var response = await client.PostAsync("http://localhost:5298/api/Auth/Logout", content);

        await HttpContext.SignOutAsync(JwtBearerDefaults.AuthenticationScheme);
        return RedirectToAction("Index", "Home");
    }
    public async Task<IActionResult> UpdatePasswordPolicy()
    {
        return View();
    }
    public IActionResult ExcelDownload()
    {
        using (var workbook = new XLWorkbook())
        {
            var worksheet = workbook.Worksheets.Add("Users Register");

            // Başlık satırı
            worksheet.Cell(1, 1).Value = "FullName";
            worksheet.Cell(1, 2).Value = "Email";
            worksheet.Cell(1, 3).Value = "Password";
            worksheet.Cell(1, 4).Value = "PasswordConfirm";


            using (var stream = new MemoryStream())
            {
                workbook.SaveAs(stream);
                stream.Position = 0;

                // Kullanıcıya indirme olarak sunma
                return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Users.xlsx");
            }
        }
    }
    public IActionResult ExcelUpload()
    {
        return View();
    }
}

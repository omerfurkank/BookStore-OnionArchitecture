using Application.Features.Auth.Commands.Login;
using Application.Features.Auth.Commands.Logout;
using Application.Features.Auth.Commands.RefreshToken;
using Application.Features.Auth.Commands.Register;
using Application.Features.Auth.Commands.RegisterRange;
using Application.Features.Auth.Commands.UpdatePasswordPolicy;
using Application.Features.Auth.Queries.GetPasswordPolicy;
using Azure.Core;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;
[Route("api/[controller]")]
[ApiController]
public class AuthController : BaseController
{
    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterCommandRequest request)
    {
        var response = await Mediator.Send(request);
        return Ok(response);
    }
    [HttpPost("registerRange")]
    public async Task<IActionResult> RegisterRange(RegisterRangeCommandRequest request)
    {
        var response = await Mediator.Send(request);
        return Ok(response);
    }
    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginCommandRequest request)
    {
        var response = await Mediator.Send(request);
        return Ok(response);
    }
    [HttpPost("refreshTokenLogin")]
    public async Task<IActionResult> RefreshTokenLogin(RefreshTokenCommandRequest request)
    {
        var response = await Mediator.Send(request);
        return Ok(response);
    }
    [HttpPost("logout")]
    public async Task<IActionResult> Logout(LogoutCommandRequest request)
    {
        var response = await Mediator.Send(request);
        //await HttpContext.SignOutAsync(JwtBearerDefaults.AuthenticationScheme);
        return Ok(response);
    }
    [HttpPut("logoutAll")]
    public async Task<IActionResult> LogoutAll()
    {
        var response = await Mediator.Send(new LogoutCommandRequest());
        return Ok(response);
    }
    [HttpPut("updatePasswordPolicy")]
    public async Task<IActionResult> UpdatePasswordPolicy(UpdatePasswordPolicyCommandRequest request)
    {
        var response = await Mediator.Send(request);
        return Ok(response);
    }
    [HttpGet("getPasswordPolicy")]
    public async Task<IActionResult> GetPasswordPolicy()
    {
        var response = await Mediator.Send(new GetPasswordPolicyQueryRequest());
        return Ok(response);
    }
}

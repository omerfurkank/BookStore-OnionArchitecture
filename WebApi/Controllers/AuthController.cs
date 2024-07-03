using Application.Features.Auth.Commands.Login;
using Application.Features.Auth.Commands.Logout;
using Application.Features.Auth.Commands.RefreshToken;
using Application.Features.Auth.Commands.Register;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;
[Route("api/[controller]")]
[ApiController]
public class AuthController : BaseController
{
    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterCommandRequest request)
    {
        return Ok(await Mediator.Send(request));
    }
    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginCommandRequest request)
    {
        return Ok(await Mediator.Send(request));
    }
    [HttpPut("refreshTokenLogin")]
    public async Task<IActionResult> RefreshTokenLogin(RefreshTokenCommandRequest request)
    {
        return Ok(await Mediator.Send(request));
    }
    [HttpPost("logout")]
    public async Task<IActionResult> Logout(LogoutCommandRequest request)
    {
        return Ok(await Mediator.Send(request));
    }
    [HttpPut("logoutAll")]
    public async Task<IActionResult> LogoutAll()
    {
        return Ok(await Mediator.Send(new LogoutCommandRequest()));
    }
}

using Application.Features.Auth.Commands.Login;
using Application.Features.Auth.Commands.Logout;
using Application.Features.Auth.Commands.LogoutAll;
using Application.Features.Auth.Commands.RefreshToken;
using Application.Features.Auth.Commands.Register;
using MediatR;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;
[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IMediator _mediator;

    public AuthController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterCommandRequest request)
    {
        var response = await _mediator.Send(request);
        return Ok(response);
    }
    [HttpPut("login")]
    public async Task<IActionResult> Login(LoginCommandRequest request)
    {
        var response = await _mediator.Send(request);
        return Ok(response);
    }
    [HttpPut("refreshTokenLogin")]
    public async Task<IActionResult> RefreshTokenLogin(RefreshTokenCommandRequest request)
    {
        var response = await _mediator.Send(request);
        return Ok(response);
    }
    [HttpPut("logout")]
    public async Task<IActionResult> Logout(LogoutCommandRequest request)
    {
        var response = await _mediator.Send(request);
        return Ok(response);
    }
    [HttpPut("logoutAll")]
    public async Task<IActionResult> LogoutAll()
    {
        var response = await _mediator.Send(new LogoutAllCommandRequest());
        return Ok(response);
    }
}

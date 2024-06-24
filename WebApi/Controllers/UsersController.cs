using Application.Features.Users.Commands.AssignRoleToUser;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;
[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly IMediator _mediator;

    public UsersController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpPost("addRolesToUser")]
    public async Task<IActionResult> Add([FromBody] AssignRoleToUserCommandRequest command)
    {
        AssignRoleToUserCommandResponse result = await _mediator.Send(command);
        return Created("", result);
    }
}

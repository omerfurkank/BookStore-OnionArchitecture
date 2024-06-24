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
    public async Task<IActionResult> Add([FromBody] AssignRoleToUserCommandRequest request)
    {
        AssignRoleToUserCommandResponse response = await _mediator.Send(request);
        return Created("", response);
    }
}

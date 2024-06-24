using Application.Features.Roles.Commands.CreateRole;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;
[Route("api/[controller]")]
[ApiController]
public class RolesController : ControllerBase
{
    private readonly IMediator _mediator;

    public RolesController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpPost("add")]
    public async Task<IActionResult> Add([FromBody] CreateRoleCommandRequest request)
    {
        CreateRoleCommandResponse response = await _mediator.Send(request);
        return Created("", response);
    }
}

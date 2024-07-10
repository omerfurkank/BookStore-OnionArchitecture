using Application.Features.Users.Commands.AssignRoleToUser;
using Application.Features.Users.Queries.GetListUser;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;
[Route("api/[controller]")]
[ApiController]
public class UsersController : BaseController
{
    [HttpPost("addRolesToUser")]
    public async Task<IActionResult> Add([FromBody] AssignRoleToUserCommandRequest request)
    {
        var response = await Mediator.Send(request);
        return Created("", response);
    }
    [HttpGet("getList")]
    public async Task<IActionResult> GetList([FromQuery] GetListUserQueryRequest request)
    {
        var response = await Mediator.Send(request);
        return Ok(response);
    }
}

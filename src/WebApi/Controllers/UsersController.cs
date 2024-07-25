using Application.Features.Users.Commands.UpdateUserWithRoles;
using Application.Features.Users.Queries.GetByIdUser;
using Application.Features.Users.Queries.GetListUser;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;
[Route("api/[controller]")]
[ApiController]
public class UsersController : BaseController
{
    [HttpGet("getList")]
    public async Task<IActionResult> GetList([FromQuery] GetListUserQueryRequest request)
    {
        var response = await Mediator.Send(request);
        return Ok(response);
    }
    [HttpGet("{Id}")]
    public async Task<IActionResult> GetById([FromRoute] GetByIdUserQueryRequest request)
    {
        var response = await Mediator.Send(request);
        return Ok(response);
    }
    [HttpPut("updateUserWithRoles")]
    public async Task<IActionResult> UpdateUserWithRoles([FromBody] UpdateUserWithRolesCommandRequest request)
    {
        var response = await Mediator.Send(request);
        return Ok(response);
    }
}

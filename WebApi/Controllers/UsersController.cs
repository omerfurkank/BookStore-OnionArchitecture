using Application.Features.Users.Commands.AssignRoleToUser;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;
[Route("api/[controller]")]
[ApiController]
public class UsersController : BaseController
{
    [HttpPost("addRolesToUser")]
    public async Task<IActionResult> Add([FromBody] AssignRoleToUserCommandRequest request)
    {
        return Created("", await Mediator.Send(request));
    }
}

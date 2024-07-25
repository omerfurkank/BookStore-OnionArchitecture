using Application.Features.Roles.Commands.CreateRole;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;
[Route("api/[controller]")]
[ApiController]
public class RolesController : BaseController
{
    [HttpPost("add")]
    public async Task<IActionResult> Add([FromBody] CreateRoleCommandRequest request)
    {
        return Created("", await Mediator.Send(request));
    }
}

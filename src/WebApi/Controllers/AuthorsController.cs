using Application.Features.Authors.Commands.CreateAuthor;
using Application.Features.Authors.Commands.CreateRangeAuthor;
using Application.Features.Authors.Commands.DeleteAuthor;
using Application.Features.Authors.Commands.UpdateAuthor;
using Application.Features.Authors.Queries.GetByIdAuthor;
using Application.Features.Authors.Queries.GetListAuthor;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;
[Route("api/[controller]")]
[ApiController]
public class AuthorsController : BaseController
{
    [HttpGet("getList")]
    public async Task<IActionResult> GetList([FromQuery] GetListAuthorQueryRequest request)
    {
        var response = await Mediator.Send(request);
        return Ok(response);
    }
    [HttpGet("{Id}")]
    public async Task<IActionResult> GetById([FromRoute] GetByIdAuthorQueryRequest request)
    {
        var response = await Mediator.Send(request);
        return Ok(response);
    }
    [HttpPost("add")]
    public async Task<IActionResult> Add([FromForm] CreateAuthorCommandRequest request)
    {
        var response = await Mediator.Send(request);
        return Created("", response);
    }
    [HttpPost("addRange")]
    public async Task<IActionResult> AddRange([FromBody] CreateRangeAuthorCommandRequest request)
    {
        var response = await Mediator.Send(request);
        return Created("", response);
    }
    [HttpPut("update")]
    public async Task<IActionResult> Update([FromForm] UpdateAuthorCommandRequest request)
    {
        var response = await Mediator.Send(request);
        return Ok(response);
    }
    [HttpDelete("{Id}")]
    public async Task<IActionResult> Delete([FromRoute] DeleteAuthorCommandRequest request)
    {
        var response = await Mediator.Send(request);
        return Ok(response);
    }
}

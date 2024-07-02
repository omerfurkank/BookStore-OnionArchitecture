using Application.Features.Authors.Commands.CreateAuthor;
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
    [HttpGet()]
    public async Task<IActionResult> GetList([FromQuery] GetListAuthorQueryRequest request)
    {
        return Ok(await Mediator.Send(request));
    }
    [HttpGet("{Id}",Name ="getAuthorById")]
    public async Task<IActionResult> GetById([FromRoute] GetByIdAuthorQueryRequest request)
    {
        return Ok(await Mediator.Send(request));
    }
    [HttpPost()]
    public async Task<IActionResult> Add([FromBody] CreateAuthorCommandRequest request)
    {
        return Created("", await Mediator.Send(request));
    }
    [HttpPut()]
    public async Task<IActionResult> Update([FromBody] UpdateAuthorCommandRequest request)
    {
        return Ok(await Mediator.Send(request));
    }
    [HttpDelete("{Id}")]
    public async Task<IActionResult> Delete([FromBody] DeleteAuthorCommandRequest request)
    {
        return Ok(await Mediator.Send(request));
    }
}

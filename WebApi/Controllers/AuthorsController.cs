using Application.Features.Authors.Commands.CreateAuthor;
using Application.Features.Authors.Commands.DeleteAuthor;
using Application.Features.Authors.Commands.UpdateAuthor;
using Application.Features.Authors.Queries.GetByIdAuthor;
using Application.Features.Authors.Queries.GetListAuthor;
using Application.Features.Books.Commands.CreateBook;
using Application.Features.Books.Commands.DeleteBook;
using Application.Features.Books.Commands.UpdateBook;
using Application.Features.Books.Queries.GetByIdBook;
using Azure.Core;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;
[Route("api/[controller]")]
[ApiController]
public class AuthorsController : ControllerBase
{
    private readonly IMediator _mediator;

    public AuthorsController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpGet("getList")]
    public async Task<IActionResult> GetList([FromQuery] GetListAuthorQueryRequest request)
    {
        IList<GetListAuthorQueryResponse> response = await _mediator.Send(request);
        return Ok(response);
    }
    [HttpGet("{Id}",Name ="getAuthorById")]
    public async Task<IActionResult> GetById([FromRoute] GetByIdAuthorQueryRequest request)
    {
        GetByIdAuthorQueryResponse response = await _mediator.Send(request);
        return Ok(response);
    }
    [HttpPost("add")]
    public async Task<IActionResult> Add([FromBody] CreateAuthorCommandRequest request)
    {
        CreateAuthorCommandResponse response = await _mediator.Send(request);
        return Created("", response);
    }
    [HttpPut("update")]
    public async Task<IActionResult> Update([FromBody] UpdateAuthorCommandRequest request)
    {
        UpdateAuthorCommandResponse response = await _mediator.Send(request);
        return Ok(response);
    }
    [HttpDelete("delete")]
    public async Task<IActionResult> Delete([FromBody] DeleteAuthorCommandRequest request)
    {
        DeleteAuthorCommandResponse response = await _mediator.Send(request);
        return Ok(response);
    }
}

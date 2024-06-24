using Application.Features.Books.Commands.CreateBook;
using Application.Features.Books.Commands.DeleteBook;
using Application.Features.Books.Commands.UpdateBook;
using Application.Features.Books.Queries.GetByIdBook;
using Application.Features.Books.Queries.GetListBook;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;
[Route("api/[controller]")]
[ApiController]
public class BooksController : ControllerBase
{
    private readonly IMediator _mediator;

    public BooksController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("getList")]
    public async Task<IActionResult> GetList([FromQuery] GetListBookQueryRequest request)
    {
        IList<GetListBookQueryResponse> response = await _mediator.Send(request);
        return Ok(response);
    }
    [HttpGet("{Id}",Name = "getBookById")]
    public async Task<IActionResult> GetById([FromRoute] GetByIdBookQueryRequest request)
    {
        GetByIdBookQueryResponse response = await _mediator.Send(request);
        return Ok(response);
    }
    //[Authorize]
    [HttpPost("add")]
    public async Task<IActionResult> Add([FromBody] CreateBookCommandRequest request)
    {
        CreateBookCommandResponse response = await _mediator.Send(request);
        return Created("", response);
    }
    [HttpPut("update")]
    public async Task<IActionResult> Update([FromBody] UpdateBookCommandRequest request)
    {

        UpdateBookCommandResponse response = await _mediator.Send(request);
        return Ok(response);
    }
    [HttpDelete("delete")]
    public async Task<IActionResult> Delete([FromBody] DeleteBookCommandRequest request)
    {
        DeleteBookCommandResponse response = await _mediator.Send(request);
        return Ok(response);
    }
}

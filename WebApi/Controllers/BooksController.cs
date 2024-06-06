using Application.Features.Books.Commands.CreateBook;
using Application.Features.Books.Commands.DeleteBook;
using Application.Features.Books.Commands.UpdateBook;
using Application.Features.Books.Queries.GetByIdBook;
using Application.Features.Books.Queries.GetListBook;
using AutoMapper;
using MediatR;
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

    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] GetListBookQueryRequest request)
    {
        IList<GetListBookQueryResponse> result = await _mediator.Send(request);
        return Ok(result);
    }
    [HttpGet("{Id}")]
    public async Task<IActionResult> Get([FromRoute] GetByIdBookQueryRequest getByIdProductQuery)
    {
        GetByIdBookQueryResponse result = await _mediator.Send(getByIdProductQuery);
        return Ok(result);
    }
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CreateBookCommandRequest command)
    {
        CreateBookCommandResponse result = await _mediator.Send(command);
        return Created("", result);
    }
    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateBookCommandRequest command)
    {

        UpdateBookCommandResponse result = await _mediator.Send(command);
        return Ok(result);
    }
    [HttpDelete]
    public async Task<IActionResult> Delete([FromBody] DeleteBookCommandRequest command)
    {
        DeleteBookCommandResponse result = await _mediator.Send(command);
        return Ok(result);
    }
}

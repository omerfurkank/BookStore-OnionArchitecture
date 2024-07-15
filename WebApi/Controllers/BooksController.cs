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
public class BooksController : BaseController
{
    [HttpGet("getList")]
    public async Task<IActionResult> GetList([FromQuery] GetListBookQueryRequest request)
    {
        var response = await Mediator.Send(request);
        return Ok(response);
    }
    [HttpGet("{Id}",Name = "getBookById")]
    public async Task<IActionResult> GetById([FromRoute] GetByIdBookQueryRequest request)
    {
        var response = await Mediator.Send(request);
        return Ok(response);
    }
    [HttpPost("add")]
    public async Task<IActionResult> Add([FromForm] CreateBookCommandRequest request)
    {
        var response = await Mediator.Send(request);
        return Created("", response);
    }
    [HttpPut("update")]
    public async Task<IActionResult> Update([FromForm] UpdateBookCommandRequest request)
    {
        var response = await Mediator.Send(request);
        return Ok(response);
    }
    [HttpDelete("{Id}", Name = "deleteBook")]
    public async Task<IActionResult> Delete([FromRoute] DeleteBookCommandRequest request)
    {
        var response = await Mediator.Send(request);
        return Ok(response);
    }
}

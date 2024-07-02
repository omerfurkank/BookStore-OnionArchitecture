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
        return Ok(await Mediator.Send(request));
    }
    [HttpGet("{Id}",Name = "getBookById")]
    public async Task<IActionResult> GetById([FromRoute] GetByIdBookQueryRequest request)
    {
        return Ok(await Mediator.Send(request));
    }
    [HttpPost("add")]
    public async Task<IActionResult> Add([FromBody] CreateBookCommandRequest request)
    {
        return Created("", await Mediator.Send(request));
    }
    [HttpPut("update")]
    public async Task<IActionResult> Update([FromBody] UpdateBookCommandRequest request)
    {
        return Ok(await Mediator.Send(request));
    }
    [HttpDelete("delete")]
    public async Task<IActionResult> Delete([FromQuery] DeleteBookCommandRequest request)
    {
        return Ok(await Mediator.Send(request));
    }
}

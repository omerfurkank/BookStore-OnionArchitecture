using Application.Features.Authors.Commands.CreateAuthor;
using Application.Features.Authors.Commands.DeleteAuthor;
using Application.Features.Authors.Commands.UpdateAuthor;
using Application.Features.Authors.Queries.GetByIdAuthor;
using Application.Features.Books.Commands.CreateBook;
using Application.Features.Books.Commands.DeleteBook;
using Application.Features.Books.Commands.UpdateBook;
using Application.Features.Books.Queries.GetByIdBook;
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
    //[HttpGet]
    //public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
    //{
    //    IList<GetListBookQueryResponse> result = await _mediator.Send(
    //        new GetListProductQuery() { PageRequest = pageRequest });
    //    return Ok(result);
    //}
    [HttpGet("{Id}")]
    public async Task<IActionResult> Get([FromRoute] GetByIdAuthorQueryRequest getByIdAuthorQuery)
    {
        GetByIdAuthorQueryResponse result = await _mediator.Send(getByIdAuthorQuery);
        return Ok(result);
    }
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CreateAuthorCommandRequest command)
    {
        CreateAuthorCommandResponse result = await _mediator.Send(command);
        return Created("", result);
    }
    //[HttpPut]
    //public async Task<IActionResult> Update([FromBody] UpdateAuthorCommandRequest command)
    //{
    //    UpdateAuthorCommandResponse result = await _mediator.Send(command);
    //    return Ok(result);
    //}
    //[HttpDelete]
    //public async Task<IActionResult> Delete([FromBody] DeleteAuthorCommandRequest command)
    //{
    //    DeleteAuthorCommandResponse result = await _mediator.Send(command);
    //    return Ok(result);
    //}
}

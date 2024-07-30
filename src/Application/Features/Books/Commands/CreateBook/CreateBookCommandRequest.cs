using Application.Pipelines.Auth;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Features.Books.Commands.CreateBook;

public class CreateBookCommandRequest : IRequest<CreateBookCommandResponse>, ISecuredRequest
{
    public int AuthorId { get; set; }
    public string? Name { get; set; }
    public IFormFile? ImageUrl { get; set; }
    public string[] Roles => new[] {"admin"};
}

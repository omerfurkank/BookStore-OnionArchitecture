using Application.Pipelines.Auth;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Features.Authors.Commands.CreateAuthor;

public class CreateAuthorCommandRequest : IRequest<CreateAuthorCommandResponse>, ISecuredRequest
{
    public string Name { get; set; }
    public IFormFile ImageUrl { get; set; }

    public string[] Roles => new[] { "admin" };
}

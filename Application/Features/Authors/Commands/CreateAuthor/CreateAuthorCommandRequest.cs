using Application.Pipelines.Auth;
using MediatR;

namespace Application.Features.Authors.Commands.CreateAuthor;

public class CreateAuthorCommandRequest : IRequest<CreateAuthorCommandResponse>, ISecuredRequest
{
    public string Name { get; set; }

    public string[] Roles => new[] { "admin" };
}

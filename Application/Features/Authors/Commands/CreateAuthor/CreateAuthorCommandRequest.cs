using MediatR;

namespace Application.Features.Authors.Commands.CreateAuthor;

public class CreateAuthorCommandRequest : IRequest<CreateAuthorCommandResponse>
{
    public string Name { get; set; }
}

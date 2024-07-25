using Application.Pipelines.Auth;
using MediatR;

namespace Application.Features.Authors.Queries.GetByIdAuthor;

public class GetByIdAuthorQueryRequest : IRequest<GetByIdAuthorQueryResponse>,ISecuredRequest
{
    public  int Id { get; set; }

    public string[] Roles => new[] {"admin"};
}

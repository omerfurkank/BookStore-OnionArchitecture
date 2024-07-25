using Application.Pipelines.Auth;
using Application.Pipelines.Logging;
using MediatR;

namespace Application.Features.Books.Queries.GetByIdBook;

public class GetByIdBookQueryRequest : IRequest<GetByIdBookQueryResponse>, ILoggableRequest, ISecuredRequest
{
    public int Id { get; set; }

    public string[] Roles => new[] {"admin"};
}

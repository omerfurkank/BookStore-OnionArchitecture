using Application.Pipelines.Logging;
using MediatR;

namespace Application.Features.Books.Queries.GetByIdBook;

public class GetByIdBookQueryRequest : IRequest<GetByIdBookQueryResponse>, ILoggableRequest
{
    public int Id { get; set; }
}

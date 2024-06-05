using MediatR;

namespace Application.Features.Books.Queries.GetByIdBook;

public class GetByIdBookQueryRequest : IRequest<GetByIdBookQueryResponse>
{
    public int Id { get; set; }
}

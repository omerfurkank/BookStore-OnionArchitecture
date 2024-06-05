using MediatR;

namespace Application.Features.Authors.Queries.GetByIdAuthor;

public class GetByIdAuthorQueryRequest : IRequest<GetByIdAuthorQueryResponse>
{
    public  int Id { get; set; }
}

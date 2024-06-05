using MediatR;

namespace Application.Features.Authors.Queries.GetByIdAuthor;

public class GetByIdAuthorQueryHandler : IRequestHandler<GetByIdAuthorQueryRequest, GetByIdAuthorQueryResponse>
{
    public Task<GetByIdAuthorQueryResponse> Handle(GetByIdAuthorQueryRequest request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}

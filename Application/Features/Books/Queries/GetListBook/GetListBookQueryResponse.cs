using Application.Features.Books.Queries.GetByIdBook;
using Domain.Entities;

namespace Application.Features.Books.Queries.GetListBook;

public class GetListBookQueryResponse
{
    public IList<GetByIdBookQueryResponse> Books { get; set; }
}

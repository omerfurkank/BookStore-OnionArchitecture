using Application.Features.Books.Queries.GetByIdBook;
using Domain.Entities;

namespace Application.Features.Books.Queries.GetListBook;

public class GetListBookQueryResponse
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string AuthorName { get; set; }
}

namespace Application.Features.Books.Queries.GetByIdBook;

public class GetByIdBookQueryResponse
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public int AuthorId { get; set; }
    //public string AuthorName { get; set; }
    public string ImageUrl { get; set; }
}

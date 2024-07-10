namespace Application.Features.Books.Commands.DeleteBook;

public class DeleteBookCommandResponse
{
    public int Id { get; set; }
    public int AuthorId { get; set; }
    public string? Name { get; set; }
}

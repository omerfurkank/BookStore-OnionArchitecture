namespace Application.Features.Books.Commands.CreateBook;

public class CreateBookCommandResponse
{
    public int Id { get; set; }
    public int AuthorId { get; set; }
    public string Name { get; set; }
}

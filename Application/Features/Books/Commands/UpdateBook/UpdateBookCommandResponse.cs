namespace Application.Features.Books.Commands.UpdateBook;

public class UpdateBookCommandResponse
{
    public int Id { get; set; }
    public int AuthorId { get; set; }
    public string Name { get; set; }
}

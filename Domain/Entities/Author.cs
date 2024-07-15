using Domain.Common;

namespace Domain.Entities;

public class Author : Entity
{
    public string Name { get; set; }
    public string? ImageUrl { get; set; }
    public IList<Book>? Books { get; set; }
    public Author()
    {
           Books = new List<Book>();  
    }
}

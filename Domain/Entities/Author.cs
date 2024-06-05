using Domain.Common;

namespace Domain.Entities;

public class Author : Entity
{
    public string Name { get; set; }
    public IList<Book>? Books { get; set; }
    public Author()
    {
            
    }

    public Author(int id, string name, IList<Book>? books)
    {
        Id = id;
        Name = name;
        Books = books;
    }
}

using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities;

public class Book : Entity
{
    public string Name { get; set; }
    public int AuthorId { get; set; }
    public string? ImageUrl { get; set; }
    public Author Author { get; set; }

}

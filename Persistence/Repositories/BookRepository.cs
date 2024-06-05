using Application.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Contexts;
using Persistence.Repositories.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories;
public class BookRepository : EFRepository<Book>, IBookRepository
{
    public BookRepository(BookDbContext context) : base(context)
    {
    }
}

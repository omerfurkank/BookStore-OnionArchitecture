using Application.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Contexts;
using Persistence.Repositories.Common;

namespace Persistence.Repositories;

public class AuthorRepository : EFRepository<Author>, IAuthorRepository
{
    public AuthorRepository(BookDbContext context) : base(context)
    {
    }
}

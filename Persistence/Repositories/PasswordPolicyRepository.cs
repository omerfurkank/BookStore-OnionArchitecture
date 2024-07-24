using Application.Repositories;
using Domain.Entities;
using Persistence.Contexts;
using Persistence.Repositories.Common;

namespace Persistence.Repositories;

public class PasswordPolicyRepository : EFRepository<PasswordPolicy>, IPasswordPolicyRepository
{
    public PasswordPolicyRepository(BookDbContext context) : base(context)
    {
    }
}

using Domain.Common;

namespace Domain.Entities;

public class PasswordPolicy : Entity
{
    public bool RequireDigit { get; set; }
    public bool RequireLowerCase { get; set; }
    public bool RequireUpperCase { get; set; }
    public bool RequireNonAlphanumeric { get; set; }
    public int RequiredLength { get; set; }
    public PasswordPolicy()
    {
        
    }

    public PasswordPolicy(int id, DateTime createdDate, DateTime? updatedDate, bool requireDigit, bool requireLowerCase, bool requireUpperCase, bool requireNonAlphanumeric, int requiredLength)
    {
        Id = id;
        CreatedDate = createdDate;
        UpdatedDate = updatedDate;
        RequireDigit = requireDigit;
        RequireLowerCase = requireLowerCase;
        RequireUpperCase = requireUpperCase;
        RequireNonAlphanumeric = requireNonAlphanumeric;
        RequiredLength = requiredLength;
    }
}
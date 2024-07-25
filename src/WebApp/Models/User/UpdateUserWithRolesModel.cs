namespace WebApp.Models.User;

public class UpdateUserWithRolesModel
{
    public int Id { get; set; }
    public string? FullName { get; set; }
    public string? Email { get; set; }
    public string[]? Roles { get; set; }
}

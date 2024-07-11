namespace Application.Features.Users.Queries.GetByIdUser;

public class GetByIdUserQueryResponse
{
    public int Id { get; set; }
    public string? FullName { get; set; }
    public string? Email { get; set; }
    public IList<string>? Roles { get; set; }
    public IList<string>? AllRoles { get; set; }
}

using Application.Repositories;
using Domain.Entities.Identity;
using MediatR;

namespace Application.Features.Users.Queries.GetByIdUser;

public class GetByIdUserQueryHandler : IRequestHandler<GetByIdUserQueryRequest, GetByIdUserQueryResponse>
{
    private readonly IUserRepository _userRepository;
    private readonly IRoleRepository _roleRepository;
    public GetByIdUserQueryHandler(IUserRepository userRepository, IRoleRepository roleRepository)
    {
        _userRepository = userRepository;
        _roleRepository = roleRepository;
    }

    public async Task<GetByIdUserQueryResponse> Handle(GetByIdUserQueryRequest request, CancellationToken cancellationToken)
    {
        User? user = await _userRepository.GetUserByIdAsync(request.Id);
        IList<string> roles = await _userRepository.GetUserRolesAsync(user);
        IList<string> allroles = _roleRepository.GetList();

        GetByIdUserQueryResponse response = new()
        {
            Id = user.Id,
            FullName = user.FullName,
            Email = user.Email,
            Roles = roles,
            AllRoles = allroles
        };
        return response;
    }
}

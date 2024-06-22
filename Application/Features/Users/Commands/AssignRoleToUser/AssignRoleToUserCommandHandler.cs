using Application.Repositories;
using Domain.Entities.Identity;
using MediatR;

namespace Application.Features.Users.Commands.AssignRoleToUser;

public class AssignRoleToUserCommandHandler : IRequestHandler<AssignRoleToUserCommandRequest, AssignRoleToUserCommandResponse>
{
    private readonly IUserRepository _userRepository;

    public AssignRoleToUserCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<AssignRoleToUserCommandResponse> Handle(AssignRoleToUserCommandRequest request, CancellationToken cancellationToken)
    {
        User? user = await _userRepository.GetUserByIdAsync(request.UserId.ToString());
        var result = await _userRepository.AddRolesToUserAsync(user.Id, request.RolesToBeAdded);
        return new();
    }
}

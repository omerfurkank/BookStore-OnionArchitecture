using Application.Repositories;
using AutoMapper;
using Domain.Entities.Identity;
using MediatR;

namespace Application.Features.Users.Commands.UpdateUserWithRoles;

public class UpdateUserWithRolesCommandHandler : IRequestHandler<UpdateUserWithRolesCommandRequest, UpdateUserWithRolesCommandResponse>
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public UpdateUserWithRolesCommandHandler(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<UpdateUserWithRolesCommandResponse> Handle(UpdateUserWithRolesCommandRequest request, CancellationToken cancellationToken)
    {
        User? user = await _userRepository.GetUserByIdAsync(request.Id);
        user = _mapper.Map(request, user);
        await _userRepository.UpdateUserAsync(user);
        await _userRepository.AddRolesToUserAsync(user.Id, request.Roles);
        return new UpdateUserWithRolesCommandResponse();
    }
}

using Application.Repositories;
using Domain.Entities.Identity;
using MediatR;

namespace Application.Features.Auth.Commands.Logout;

public class LogoutCommandHandler : IRequestHandler<LogoutCommandRequest, LogoutCommandResponse>
{
    private readonly IUserRepository _userRepository;

    public LogoutCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<LogoutCommandResponse> Handle(LogoutCommandRequest request, CancellationToken cancellationToken)
    {
        User? user = await _userRepository.GetUserByEmailAsync(request.Email);

        user.RefreshToken = null;
        await _userRepository.UpdateUserAsync(user);
        return new LogoutCommandResponse();
    }
}


using Application.Repositories;
using Domain.Entities.Identity;
using MediatR;

namespace Application.Features.Auth.Commands.LogoutAll;

public class LogoutAllCommandHandler : IRequestHandler<LogoutAllCommandRequest, LogoutAllCommandResponse>
{
    private readonly IUserRepository _userRepository;

    public LogoutAllCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<LogoutAllCommandResponse> Handle(LogoutAllCommandRequest request, CancellationToken cancellationToken)
    {
        IList<User> users = await _userRepository.GetAllUserAsync();

        foreach (var user in users)
        {
            user.RefreshToken = null;
            user.RefreshTokenExpiredTime = null;
            await _userRepository.UpdateUserAsync(user);
        }

        return new LogoutAllCommandResponse();
    }
}

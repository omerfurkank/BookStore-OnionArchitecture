using Application.Features.Auth.Rules.BusinessRules;
using Application.Repositories;
using Domain.Entities.Identity;
using Infrastructure.Tokens;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Application.Features.Auth.Commands.RefreshToken;

public class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommandRequest, RefreshTokenCommandResponse>
{
    private readonly IUserRepository _userRepository;
    private readonly ITokenService _tokenService;
    private readonly AuthBusinessRules _authBusinessRules;

    public RefreshTokenCommandHandler(IUserRepository userRepository, ITokenService tokenService, AuthBusinessRules authBusinessRules)
    {
        _userRepository = userRepository;
        _tokenService = tokenService;
        _authBusinessRules = authBusinessRules;
    }

    public async Task<RefreshTokenCommandResponse> Handle(RefreshTokenCommandRequest request, CancellationToken cancellationToken)
    {
        ClaimsPrincipal? principal = _tokenService.GetPrincipalFromExpiredToken(request.AccessToken);
        string? email = principal.FindFirstValue(ClaimTypes.Email);
        User? user = await _userRepository.GetUserByEmailAsync(email);
        IList<string> roles = await _userRepository.GetUserRolesAsync(user);

        await _authBusinessRules.CheckRefreshTokenExpiredDate(user.RefreshTokenExpiredTime);

        JwtSecurityToken newAccessToken = await _tokenService.CreateToken(user, roles);
        string newRefreshToken = _tokenService.GenerateRefreshToken();

        user.RefreshToken = newRefreshToken;
        await _userRepository.UpdateUserAsync(user);

        return new()
        {
            AccessToken = new JwtSecurityTokenHandler().WriteToken(newAccessToken),
            RefreshToken = newRefreshToken,
        };
    }
}

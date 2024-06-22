using Application.Exceptions.CustomExceptions;
using Application.Features.Auth.Rules.BusinessRules;
using Application.Repositories;
using AutoMapper;
using Domain.Entities.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Features.Auth.Commands.Register;

public class RegisterCommandHandler : IRequestHandler<RegisterCommandRequest, RegisterCommandResponse>
{
    private readonly AuthBusinessRules _authBusinessRules;
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public RegisterCommandHandler(AuthBusinessRules authBusinessRules, IUserRepository userRepository, IMapper mapper)
    {
        _authBusinessRules = authBusinessRules;
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<RegisterCommandResponse> Handle(RegisterCommandRequest request, CancellationToken cancellationToken)
    {
        await _authBusinessRules.CheckUserExistsToRegister(request.Email);
        User user = _mapper.Map<User>(request);
        user.UserName = request.Email;

        var result = await _userRepository.CreateUserAsync(user,request.Password);
        if (result.Succeeded)
        {
            await _userRepository.AddRolesToUserAsync(user.Id, new[] { "user" });
            var response = _mapper.Map<RegisterCommandResponse>(user);
            return response;
        }
        throw new BusinessException(result.Errors.ToString());
    }
}

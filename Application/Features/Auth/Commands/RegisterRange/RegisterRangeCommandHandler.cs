using Application.Features.Auth.Rules.BusinessRules;
using Application.Repositories;
using AutoMapper;
using Domain.Entities.Identity;
using MediatR;

namespace Application.Features.Auth.Commands.RegisterRange;

public class RegisterRangeCommandHandler : IRequestHandler<RegisterRangeCommandRequest, RegisterRangeCommandResponse>
{
    private readonly AuthBusinessRules _authBusinessRules;
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public RegisterRangeCommandHandler(IUserRepository userRepository, IMapper mapper, AuthBusinessRules authBusinessRules)
    {
        _userRepository = userRepository;
        _mapper = mapper;
        _authBusinessRules = authBusinessRules;
    }

    public async Task<RegisterRangeCommandResponse> Handle(RegisterRangeCommandRequest request, CancellationToken cancellationToken)
    {
        List<User> addedUsers = new List<User>();
        foreach (var item in request.Users)
        {
            await _authBusinessRules.CheckUserExistsToRegister(item.Email);
            User user = _mapper.Map<User>(item);
            user.UserName = item.Email;
            await _userRepository.CreateUserAsync(user, item.Password);
            addedUsers.Add(user);
        }

        var responseList = _mapper.Map<List<RegisterRangeCommandResponse.RegisterResponseDto>>(addedUsers);
        RegisterRangeCommandResponse response = new RegisterRangeCommandResponse() {Users = responseList };

        return response;
    }
}


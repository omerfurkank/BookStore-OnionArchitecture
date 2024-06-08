using Application.Exceptions.CustomExceptions;
using Application.Features.Auth.Rules.BusinessRules;
using AutoMapper;
using Domain.Entities.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Features.Auth.Commands.Register;

public class RegisterCommandHandler : IRequestHandler<RegisterCommandRequest, RegisterCommandResponse>
{
    private readonly AuthBusinessRules _authBusinessRules;
    private readonly UserManager<User> _userManager;
    private readonly RoleManager<Role> _roleManager;
    private readonly IMapper _mapper;

    public RegisterCommandHandler(AuthBusinessRules authBusinessRules, UserManager<User> userManager, RoleManager<Role> roleManager, IMapper mapper)
    {
        _authBusinessRules = authBusinessRules;
        _userManager = userManager;
        _roleManager = roleManager;
        _mapper = mapper;
    }

    public async Task<RegisterCommandResponse> Handle(RegisterCommandRequest request, CancellationToken cancellationToken)
    {
        await _authBusinessRules.CheckUserExists(request.Email);

        User user = _mapper.Map<User>(request);
        user.UserName = request.Email;

        var result = await _userManager.CreateAsync(user,request.Password);
        if (result.Succeeded)
        {
            await _userManager.AddToRoleAsync(user, "user");
            var response = _mapper.Map<RegisterCommandResponse>(result);
            return response;
        }
        throw new BusinessException();
    }
}

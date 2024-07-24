using Application.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Auth.Commands.UpdatePasswordPolicy;
public class UpdatePasswordPolicyCommandHandler : IRequestHandler<UpdatePasswordPolicyCommandRequest, UpdatePasswordPolicyCommandResponse>
{
    private readonly IPasswordPolicyRepository _passwordPolicyRepository;
    private readonly IMapper _mapper;

    public UpdatePasswordPolicyCommandHandler(IPasswordPolicyRepository passwordPolicyRepository, IMapper mapper)
    {
        _passwordPolicyRepository = passwordPolicyRepository;
        _mapper = mapper;
    }

    public async Task<UpdatePasswordPolicyCommandResponse> Handle(UpdatePasswordPolicyCommandRequest request, CancellationToken cancellationToken)
    {
        PasswordPolicy? passwordPolicy = await _passwordPolicyRepository.GetAsync(p => p.Id == 1);
        passwordPolicy = _mapper.Map(request,passwordPolicy);
        PasswordPolicy updatedPasswordPolicy = await _passwordPolicyRepository.UpdateAsync(passwordPolicy);
        var response = _mapper.Map<UpdatePasswordPolicyCommandResponse>(updatedPasswordPolicy);
        return response;
    }
}

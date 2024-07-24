using Application.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Auth.Queries.GetPasswordPolicy;
public class GetPasswordPolicyQueryHandler : IRequestHandler<GetPasswordPolicyQueryRequest,GetPasswordPolicyQueryResponse>
{
    private readonly IPasswordPolicyRepository _passwordPolicyRepository;
    private readonly IMapper _mapper;

    public GetPasswordPolicyQueryHandler(IPasswordPolicyRepository passwordPolicyRepository, IMapper mapper)
    {
        _passwordPolicyRepository = passwordPolicyRepository;
        _mapper = mapper;
    }

    public async Task<GetPasswordPolicyQueryResponse> Handle(GetPasswordPolicyQueryRequest request, CancellationToken cancellationToken)
    {
        PasswordPolicy? passwordPolicy =await _passwordPolicyRepository.GetAsync(p => p.Id == 1);
        GetPasswordPolicyQueryResponse response=_mapper.Map<GetPasswordPolicyQueryResponse>(passwordPolicy);
        return response;
    }
}

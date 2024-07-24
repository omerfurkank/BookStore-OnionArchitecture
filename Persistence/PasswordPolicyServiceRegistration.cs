using Application.Repositories;
using Domain.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace Persistence;

public static class PasswordPolicyServiceRegistration
{
    private static IPasswordPolicyRepository _passwordPolicyRepository;

    public static void Configure(IPasswordPolicyRepository passwordPolicyRepository)
    {
        _passwordPolicyRepository = passwordPolicyRepository;
    }

    public static WebApplicationBuilder AddPasswordPolicyServices(this WebApplicationBuilder builder)
    {
        if (_passwordPolicyRepository == null)
        {
            throw new InvalidOperationException("PasswordPolicyRepository has not been configured.");
        }

        PasswordPolicy policy = _passwordPolicyRepository.GetAsync(p => p.Id == 1).GetAwaiter().GetResult();
        builder.Services.Configure<IdentityOptions>(options =>
        {
            options.Password.RequireDigit = policy.RequireDigit;
            options.Password.RequireLowercase = policy.RequireLowerCase;
            options.Password.RequireNonAlphanumeric = policy.RequireNonAlphanumeric;
            options.Password.RequireUppercase = policy.RequireUpperCase;
            options.Password.RequiredLength = policy.RequiredLength;
        });

        return builder;
    }
}

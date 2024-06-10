using Application.Exceptions;
using Application.Features.Auth.Rules.BusinessRules;
using Application.Features.Auth.Rules.ValidationRules;
using Application.Features.Books.Rules.BusinessRules;
using Application.Features.Books.Rules.ValidationRules;
using Application.Features.Roles.Rules.BusinessRules;
using Application.Pipelines;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Application;
public static class ApplicationServiceRegistration
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));

        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        services.AddValidatorsFromAssemblyContaining<CreateBookCommandRequestValidator>();
        services.AddValidatorsFromAssemblyContaining<UpdateBookCommandRequestValidator>();

        services.AddValidatorsFromAssemblyContaining<RegisterCommandRequestValidator>();

        services.AddTransient<ExceptionMiddleware>();

        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

        services.AddScoped<BookBusinessRules>();
        services.AddScoped<AuthBusinessRules>();
        services.AddScoped<RoleBusinessRules>();

        return services;
    }
}

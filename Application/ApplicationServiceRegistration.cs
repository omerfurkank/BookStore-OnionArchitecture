using Application.Exceptions;
using Application.Features.Books.Rules.BusinessRules;
using Application.Features.Books.Rules.ValidationRules;
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

        services.AddTransient<ExceptionMiddleware>();

        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

        services.AddScoped<BookBusinessRules>();

        return services;
    }
}

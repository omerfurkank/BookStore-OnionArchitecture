using Application.Repositories;
using Domain.Entities.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Contexts;
using Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Persistence;
public static class PersistenceServiceRegistration
{
    public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<BookDbContext>(options =>options.UseNpgsql(configuration.GetConnectionString("BookConnectionString")));
        services.AddIdentityCore<User>().AddRoles<Role>().AddEntityFrameworkStores<BookDbContext>();

        services.AddScoped<IAuthorRepository, AuthorRepository>();
        services.AddScoped<IBookRepository, BookRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IRoleRepository, RoleRepository>();

        services.AddScoped<IPasswordPolicyRepository, PasswordPolicyRepository>();
        PasswordPolicyServiceRegistration.Configure(services.BuildServiceProvider().GetService<IPasswordPolicyRepository>());
        return services;
    }
    
}

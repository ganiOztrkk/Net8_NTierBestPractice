using DataAccess.Context;
using DataAccess.Repositories;
using Entities.Models;
using Entities.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Scrutor;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace DataAccess;

public static class DependencyInjection
{
    public static IServiceCollection AddDataAccess(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("database");
        services.AddDbContext<ApplicationDbContext>(opt =>
        {
            opt
            .UseSqlServer(connectionString);
        });

        services
            .AddIdentityCore<AppUser>(cnf =>
            {
                cnf.Password.RequireUppercase = false;
                cnf.Password.RequireDigit = false;
                cnf.Password.RequiredLength = 4;
                cnf.Password.RequireNonAlphanumeric = false;
                cnf.Password.RequireLowercase = false;
            })
            .AddEntityFrameworkStores<ApplicationDbContext>();


        services.AddScoped<IUnitOfWork>(sv => sv.GetRequiredService<ApplicationDbContext>());
        

        services
            .Scan(selector => selector
                .FromAssemblies(
                    typeof(DependencyInjection).Assembly)
                .AddClasses(publicOnly: false)
                .UsingRegistrationStrategy(RegistrationStrategy.Skip)
                .AsMatchingInterface()
                .WithScopedLifetime());
        return services;
    }
}
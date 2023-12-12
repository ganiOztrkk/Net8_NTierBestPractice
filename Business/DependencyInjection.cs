using Business.Behaviors;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Business;

public static class DependencyInjection
{
    public static IServiceCollection AddBusiness(
        this IServiceCollection services)
    {
        services
            .AddMediatR(cnf =>
            {
                cnf
                .RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly);
                cnf.AddOpenBehavior(typeof(ValidationBehavior<,>));
            });

        services
            .AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly);

        services
            .AddAutoMapper(typeof(DependencyInjection).Assembly);

        return services;
    }
}
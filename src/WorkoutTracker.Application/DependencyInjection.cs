using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using WorkoutTracker.Application.Features.User.Commands;

namespace WorkoutTracker.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(cfg =>
            cfg.RegisterServicesFromAssemblyContaining<UserLoginCommand>());

        services.AddValidatorsFromAssemblyContaining<UserLoginCommandValidator>();

        return services;
    }
}

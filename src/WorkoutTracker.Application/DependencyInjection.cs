using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using WorkoutTracker.Application.Common.Behaviors;
using WorkoutTracker.Application.Features.Users.Commands.Login;

namespace WorkoutTracker.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssemblyContaining<UserLoginCommand>();
            cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        });

        services.AddValidatorsFromAssemblyContaining<UserLoginCommandValidator>();

        return services;
    }
}

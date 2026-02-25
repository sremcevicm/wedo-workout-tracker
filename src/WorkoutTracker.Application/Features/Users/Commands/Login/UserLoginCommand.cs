using MediatR;
using WorkoutTracker.Application.Common.DTOs;

namespace WorkoutTracker.Application.Features.Users.Commands.Login;

public record UserLoginCommand(string Email, string Password) : IRequest<AuthResponseDto>;

using MediatR;
using WorkoutTracker.Application.Common.DTOs;
using WorkoutTracker.Domain.Enums;

namespace WorkoutTracker.Application.Features.Users.Commands.Register;

public record UserRegisterCommand(
    string Email,
    string Password,
    string FirstName,
    string LastName,
    Gender Gender) : IRequest<AuthResponseDto>;

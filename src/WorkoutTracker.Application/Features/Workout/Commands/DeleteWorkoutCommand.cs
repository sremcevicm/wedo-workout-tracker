using MediatR;
namespace WorkoutTracker.Application.Features.Workout.Commands;

public record DeleteWorkoutCommand(Guid Id, Guid UserId) : IRequest;

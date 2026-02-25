using MediatR;

namespace WorkoutTracker.Application.Features.Workouts.Commands.DeleteWorkout;

public record DeleteWorkoutCommand(Guid Id) : IRequest;

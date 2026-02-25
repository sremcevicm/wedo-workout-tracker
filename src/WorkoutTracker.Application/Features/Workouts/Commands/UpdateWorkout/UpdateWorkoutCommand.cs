using MediatR;
using WorkoutTracker.Domain.Enums;

namespace WorkoutTracker.Application.Features.Workouts.Commands.UpdateWorkout;

public record UpdateWorkoutCommand(
    Guid Id,
    ExerciseType ExerciseType,
    int DurationInMinutes,
    int CaloriesBurned,
    int Difficulty,
    int Fatigue,
    DateTime WorkoutDate,
    string? Notes
) : IRequest;

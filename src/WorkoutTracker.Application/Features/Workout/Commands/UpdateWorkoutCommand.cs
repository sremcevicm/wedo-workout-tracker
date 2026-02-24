using MediatR;
using WorkoutTracker.Domain.Enums;

namespace WorkoutTracker.Application.Features.Workout.Commands;

public record UpdateWorkoutCommand(
    Guid Id,
    Guid UserId,
    ExerciseType ExerciseType,
    int DurationInMinutes,
    int CaloriesBurned,
    int Difficulty,
    int Fatigue,
    DateTime WorkoutDate,
    string? Notes
) : IRequest;
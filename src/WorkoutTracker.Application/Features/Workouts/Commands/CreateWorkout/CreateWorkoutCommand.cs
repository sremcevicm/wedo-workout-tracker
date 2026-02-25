using MediatR;
using WorkoutTracker.Domain.Enums;

namespace WorkoutTracker.Application.Features.Workouts.Commands.CreateWorkout;

public record CreateWorkoutCommand(
    ExerciseType ExerciseType,
    int DurationInMinutes,
    int CaloriesBurned,
    int Difficulty,
    int Fatigue,
    DateTime WorkoutDate,
    string? Notes) : IRequest<Guid>;

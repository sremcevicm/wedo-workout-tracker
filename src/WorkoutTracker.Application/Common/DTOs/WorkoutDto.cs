using WorkoutTracker.Domain.Enums;

namespace WorkoutTracker.Application.Common.DTOs;

public record WorkoutDto(
    Guid Id,
    ExerciseType ExerciseType,
    int DurationInMinutes,
    int CaloriesBurned,
    int Difficulty,
    int Fatigue,
    DateTime WorkoutDate,
    string? Notes);

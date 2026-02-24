namespace WorkoutTracker.Application.Common.DTOs;

public record WeeklyProgressDto(
    int Week,
    DateOnly WeekStart,
    DateOnly WeekEnd,
    int TotalDurationInMinutes,
    int TotalWorkouts,
    double AverageDifficulty,
    double AverageFatigue);
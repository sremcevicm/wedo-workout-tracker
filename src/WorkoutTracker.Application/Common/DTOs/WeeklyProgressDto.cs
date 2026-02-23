namespace WorkoutTracker.Application.Common.DTOs;

public record WeeklyProgressDto(
    int Week,
    int TotalDurationInMinutes,
    int TotalWorkouts,
    double AverageDifficulty,
    double AverageFatigue);
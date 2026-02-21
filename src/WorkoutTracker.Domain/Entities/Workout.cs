using WorkoutTracker.Domain.Enums;
using WorkoutTracker.Domain.ValueObjects;

namespace WorkoutTracker.Domain.Entities;

public class Workout
{
    public Guid Id { get; private set; }
    public Guid UserId { get; private set; }
    public ExerciseType ExerciseType { get; private set; }
    public TimeSpan Duration { get; private set; }
    public int CaloriesBurned { get; private set; }
    public Rating Difficulty { get; private set; }
    public Rating Fatigue { get; private set; }
    public string? Notes { get; private set; }
    public DateTime WorkoutDate { get; private set; }
    public DateTime CreatedAt { get; private set; }

    public User User { get; private set; } = null!;

    private Workout()
    {
        Difficulty = null!;
        Fatigue = null!;
    }

    public static Workout Create(
        Guid userId,
        ExerciseType exerciseType,
        TimeSpan duration,
        int caloriesBurned,
        int difficulty,
        int fatigue,
        DateTime workoutDate,
        string? notes = null)
    {
        return new Workout
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            ExerciseType = exerciseType,
            Duration = duration,
            CaloriesBurned = caloriesBurned,
            Difficulty = Rating.Create(difficulty),
            Fatigue = Rating.Create(fatigue),
            WorkoutDate = workoutDate,
            Notes = notes,
            CreatedAt = DateTime.UtcNow
        };
    }

    public void Update(
        ExerciseType exerciseType,
        TimeSpan duration,
        int caloriesBurned,
        int difficulty,
        int fatigue,
        DateTime workoutDate,
        string? notes)
    {
        ExerciseType = exerciseType;
        Duration = duration;
        CaloriesBurned = caloriesBurned;
        Difficulty = Rating.Create(difficulty);
        Fatigue = Rating.Create(fatigue);
        WorkoutDate = workoutDate;
        Notes = notes;
    }
}

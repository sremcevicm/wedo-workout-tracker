using FluentValidation;

namespace WorkoutTracker.Application.Features.Workouts.Commands.CreateWorkout;

public class CreateWorkoutCommandValidator : AbstractValidator<CreateWorkoutCommand>
{
    public CreateWorkoutCommandValidator()
    {
        RuleFor(x => x.ExerciseType)
            .IsInEnum().WithMessage("Invalid exercise type.");

        RuleFor(x => x.DurationInMinutes)
            .GreaterThan(0).WithMessage("Duration must be greater than 0.");

        RuleFor(x => x.CaloriesBurned)
            .GreaterThanOrEqualTo(0).WithMessage("Calories burned cannot be negative.");

        RuleFor(x => x.Difficulty)
            .InclusiveBetween(1, 10).WithMessage("Difficulty must be between 1 and 10.");

        RuleFor(x => x.Fatigue)
            .InclusiveBetween(1, 10).WithMessage("Fatigue must be between 1 and 10.");

        RuleFor(x => x.WorkoutDate)
            .LessThanOrEqualTo(DateTime.UtcNow).WithMessage("Workout date cannot be in the future.");
    }
}

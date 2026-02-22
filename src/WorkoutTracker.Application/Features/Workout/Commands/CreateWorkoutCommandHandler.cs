using MediatR;
using WorkoutTracker.Application.Interfaces;
using WorkoutEntity = WorkoutTracker.Domain.Entities.Workout;

namespace WorkoutTracker.Application.Features.Workout.Commands;

public class CreateWorkoutCommandHandler : IRequestHandler<CreateWorkoutCommand, Guid>
{
    private readonly IWorkoutRepository _workoutRepository;

    public CreateWorkoutCommandHandler(IWorkoutRepository workoutRepository)
    {
        _workoutRepository = workoutRepository;
    }

    public async Task<Guid> Handle(CreateWorkoutCommand request, CancellationToken cancellationToken)
    {
        var workout = WorkoutEntity.Create(
            request.UserId,
            request.ExerciseType,
            TimeSpan.FromMinutes(request.DurationInMinutes),
            request.CaloriesBurned,
            request.Difficulty,
            request.Fatigue,
            request.WorkoutDate,
            request.Notes);

        await _workoutRepository.AddAsync(workout);
        await _workoutRepository.SaveChangesAsync(cancellationToken);

        return workout.Id;
    }
}
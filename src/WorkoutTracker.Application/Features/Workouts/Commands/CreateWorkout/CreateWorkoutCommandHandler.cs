using MediatR;
using WorkoutTracker.Application.Interfaces;
using WorkoutTracker.Domain.Entities;

namespace WorkoutTracker.Application.Features.Workouts.Commands.CreateWorkout;

public class CreateWorkoutCommandHandler : IRequestHandler<CreateWorkoutCommand, Guid>
{
    private readonly IWorkoutRepository _workoutRepository;
    private readonly ICurrentUserService _currentUserService;

    public CreateWorkoutCommandHandler(IWorkoutRepository workoutRepository, ICurrentUserService currentUserService)
    {
        _workoutRepository = workoutRepository;
        _currentUserService = currentUserService;
    }

    public async Task<Guid> Handle(CreateWorkoutCommand request, CancellationToken cancellationToken)
    {
        var workout = Workout.Create(
            _currentUserService.UserId,
            request.ExerciseType,
            TimeSpan.FromMinutes(request.DurationInMinutes),
            request.CaloriesBurned,
            request.Difficulty,
            request.Fatigue,
            request.WorkoutDate,
            request.Notes);

        await _workoutRepository.AddAsync(workout);

        return workout.Id;
    }
}

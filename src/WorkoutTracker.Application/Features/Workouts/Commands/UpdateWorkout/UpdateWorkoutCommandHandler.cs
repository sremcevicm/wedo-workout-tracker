using MediatR;
using WorkoutTracker.Application.Interfaces;
using WorkoutTracker.Domain.Exceptions;

namespace WorkoutTracker.Application.Features.Workouts.Commands.UpdateWorkout;

public class UpdateWorkoutCommandHandler : IRequestHandler<UpdateWorkoutCommand>
{
    private readonly IWorkoutRepository _workoutRepository;
    private readonly ICurrentUserService _currentUserService;

    public UpdateWorkoutCommandHandler(IWorkoutRepository workoutRepository, ICurrentUserService currentUserService)
    {
        _workoutRepository = workoutRepository;
        _currentUserService = currentUserService;
    }

    public async Task Handle(UpdateWorkoutCommand request, CancellationToken cancellationToken)
    {
        var workout = await _workoutRepository.GetByIdAsync(request.Id);
        if (workout == null || workout.UserId != _currentUserService.UserId)
        {
            throw new NotFoundException("Workout not found");
        }

        workout.Update(request.ExerciseType,
            TimeSpan.FromMinutes(request.DurationInMinutes),
            request.CaloriesBurned,
            request.Difficulty,
            request.Fatigue,
            request.WorkoutDate,
            request.Notes);

        await _workoutRepository.UpdateAsync(workout);
    }
}

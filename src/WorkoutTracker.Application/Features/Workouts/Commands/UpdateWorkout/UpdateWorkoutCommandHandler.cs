using MediatR;
using WorkoutTracker.Application.Interfaces;
using WorkoutTracker.Domain.Exceptions;

namespace WorkoutTracker.Application.Features.Workouts.Commands.UpdateWorkout;

public class UpdateWorkoutCommandHandler : IRequestHandler<UpdateWorkoutCommand>
{
    private readonly IWorkoutRepository _workoutRepository;

    public UpdateWorkoutCommandHandler(IWorkoutRepository workoutRepository)
    {
        _workoutRepository = workoutRepository;
    }

    public async Task Handle(UpdateWorkoutCommand request, CancellationToken cancellationToken)
    {
        var workout = await _workoutRepository.GetByIdAsync(request.Id);
        if (workout == null)
            throw new NotFoundException("Workout not found");

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

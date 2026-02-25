using MediatR;
using WorkoutTracker.Application.Interfaces;
using WorkoutTracker.Domain.Exceptions;

namespace WorkoutTracker.Application.Features.Workouts.Commands.DeleteWorkout;

public class DeleteWorkoutCommandHandler : IRequestHandler<DeleteWorkoutCommand>
{
    private readonly IWorkoutRepository _workoutRepository;
    private readonly ICurrentUserService _currentUserService;

    public DeleteWorkoutCommandHandler(IWorkoutRepository workoutRepository, ICurrentUserService currentUserService)
    {
        _workoutRepository = workoutRepository;
        _currentUserService = currentUserService;
    }

    public async Task Handle(DeleteWorkoutCommand request, CancellationToken cancellationToken)
    {
        var workout = await _workoutRepository.GetByIdAsync(request.Id);
        if (workout == null || workout.UserId != _currentUserService.UserId)
        {
            throw new NotFoundException("Workout not found");
        }

        await _workoutRepository.DeleteAsync(workout);
    }
}

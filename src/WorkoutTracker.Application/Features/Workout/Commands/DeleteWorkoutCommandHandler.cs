using MediatR;
using WorkoutTracker.Application.Interfaces;
using WorkoutTracker.Domain.Exceptions;

namespace WorkoutTracker.Application.Features.Workout.Commands;

public class DeleteWorkoutCommandHandler : IRequestHandler<DeleteWorkoutCommand>
{
    private readonly IWorkoutRepository _workoutRepository;

    public DeleteWorkoutCommandHandler(IWorkoutRepository workoutRepository)
    {
        _workoutRepository = workoutRepository;
    }

    public async Task Handle(DeleteWorkoutCommand request, CancellationToken cancellationToken)
    {
        var workout = await _workoutRepository.GetByIdAsync(request.Id);
        if (workout == null || workout.UserId != request.UserId)
        {
            throw new NotFoundException("Workout not found");
        }

        await _workoutRepository.DeleteAsync(workout);
        await _workoutRepository.SaveChangesAsync(cancellationToken);
    }
}
using MediatR;
using WorkoutTracker.Application.Common.DTOs;
using WorkoutTracker.Application.Interfaces;

namespace WorkoutTracker.Application.Features.Workout.Queries;

public class GetUserWorkoutsQueryHandler : IRequestHandler<GetUserWorkoutsQuery, IEnumerable<WorkoutDto>>
{
    private readonly IWorkoutRepository _workoutRepository;

    public GetUserWorkoutsQueryHandler(IWorkoutRepository workoutRepository)
    {
        _workoutRepository = workoutRepository;
    }

    public async Task<IEnumerable<WorkoutDto>> Handle(GetUserWorkoutsQuery request, CancellationToken cancellationToken)
    {
        var workouts = await _workoutRepository.GetByUserIdAsync(request.UserId, cancellationToken);

        return workouts.Select(w => new WorkoutDto(
            w.Id,
            w.ExerciseType,
            (int)w.Duration.TotalMinutes,
            w.CaloriesBurned,
            w.Difficulty.Value,
            w.Fatigue.Value,
            w.WorkoutDate,
            w.Notes));
    }
}

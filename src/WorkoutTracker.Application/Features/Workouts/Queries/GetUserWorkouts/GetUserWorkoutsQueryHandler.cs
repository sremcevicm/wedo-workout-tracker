using MediatR;
using WorkoutTracker.Application.Common.DTOs;
using WorkoutTracker.Application.Interfaces;

namespace WorkoutTracker.Application.Features.Workouts.Queries.GetUserWorkouts;

public class GetUserWorkoutsQueryHandler : IRequestHandler<GetUserWorkoutsQuery, IEnumerable<WorkoutDto>>
{
    private readonly IWorkoutRepository _workoutRepository;
    private readonly ICurrentUserService _currentUserService;

    public GetUserWorkoutsQueryHandler(IWorkoutRepository workoutRepository, ICurrentUserService currentUserService)
    {
        _workoutRepository = workoutRepository;
        _currentUserService = currentUserService;
    }

    public async Task<IEnumerable<WorkoutDto>> Handle(GetUserWorkoutsQuery request, CancellationToken cancellationToken)
    {
        var workouts = await _workoutRepository.GetByUserIdAsync(_currentUserService.UserId, cancellationToken);
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

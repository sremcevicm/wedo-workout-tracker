using MediatR;
using WorkoutTracker.Application.Common.DTOs;
using WorkoutTracker.Application.Interfaces;

namespace WorkoutTracker.Application.Features.Workout.Queries;

public class GetMonthlyProgressQueryHandler : IRequestHandler<GetMonthlyProgressQuery, IEnumerable<WeeklyProgressDto>>
{
    private readonly IWorkoutRepository _workoutRepository;

    public GetMonthlyProgressQueryHandler(IWorkoutRepository workoutRepository)
    {
        _workoutRepository = workoutRepository;
    }

    public async Task<IEnumerable<WeeklyProgressDto>> Handle(GetMonthlyProgressQuery request, CancellationToken cancellationToken)
    {
        var weeks = GetWeeksInMonth(request.Year, request.Month).ToList();

        if (weeks.Count == 0)
            return Enumerable.Empty<WeeklyProgressDto>();

        var workouts = await _workoutRepository.GetByUserIdAndMonthAsync(
            request.UserId, request.Year, request.Month, cancellationToken);

        return weeks.Select((week, index) =>
        {
            var weekWorkouts = workouts
                .Where(w => w.WorkoutDate.Date >= week.Start && w.WorkoutDate.Date <= week.End)
                .ToList();

            return new WeeklyProgressDto(
                Week: index + 1,
                TotalDurationInMinutes: (int)weekWorkouts.Sum(w => w.Duration.TotalMinutes),
                TotalWorkouts: weekWorkouts.Count,
                AverageDifficulty: weekWorkouts.Count > 0 ? Math.Round(weekWorkouts.Average(w => w.Difficulty.Value), 1) : 0,
                AverageFatigue: weekWorkouts.Count > 0 ? Math.Round(weekWorkouts.Average(w => w.Fatigue.Value), 1) : 0);
        });
    }

    private static IEnumerable<(DateTime Start, DateTime End)> GetWeeksInMonth(int year, int month)
    {
        var firstDay = new DateTime(year, month, 1);

        var weekStart = firstDay;
        while (weekStart.DayOfWeek != DayOfWeek.Monday)
            weekStart = weekStart.AddDays(1);

        while (weekStart.Month == month)
        {
            yield return (weekStart, weekStart.AddDays(6));
            weekStart = weekStart.AddDays(7);
        }
    }
}
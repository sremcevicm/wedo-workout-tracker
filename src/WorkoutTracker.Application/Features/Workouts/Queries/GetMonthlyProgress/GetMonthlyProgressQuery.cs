using MediatR;
using WorkoutTracker.Application.Common.DTOs;

namespace WorkoutTracker.Application.Features.Workouts.Queries.GetMonthlyProgress;

public record GetMonthlyProgressQuery(int Year, int Month) : IRequest<IEnumerable<WeeklyProgressDto>>;

using MediatR;
using WorkoutTracker.Application.Common.DTOs;

namespace WorkoutTracker.Application.Features.Workout.Queries;

public record GetMonthlyProgressQuery(Guid UserId, int Year, int Month) : IRequest<IEnumerable<WeeklyProgressDto>>;
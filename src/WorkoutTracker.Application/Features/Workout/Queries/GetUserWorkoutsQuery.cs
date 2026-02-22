using MediatR;
using WorkoutTracker.Application.Common.DTOs;

namespace WorkoutTracker.Application.Features.Workout.Queries;

public record GetUserWorkoutsQuery(Guid UserId) : IRequest<IEnumerable<WorkoutDto>>;

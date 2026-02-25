using MediatR;
using WorkoutTracker.Application.Common.DTOs;

namespace WorkoutTracker.Application.Features.Workouts.Queries.GetUserWorkouts;

public record GetUserWorkoutsQuery() : IRequest<IEnumerable<WorkoutDto>>;

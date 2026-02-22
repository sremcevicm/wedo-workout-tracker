using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WorkoutTracker.Application.Features.Workout.Commands;
using WorkoutTracker.Application.Features.Workout.Queries;

namespace WorkoutTracker.API.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class WorkoutsController : ControllerBase
{
    private readonly IMediator _mediator;
    private Guid GetUserId() => Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

    public WorkoutsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> CreateWorkout([FromBody] CreateWorkoutCommand command)
    {
        var userId = GetUserId();
        var result = await _mediator.Send(command with { UserId = userId });
        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetWorkouts()
    {
        var result = await _mediator.Send(new GetUserWorkoutsQuery(GetUserId()));
        return Ok(result);
    }
}
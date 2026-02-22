using Microsoft.AspNetCore.Mvc;
using MediatR;
using WorkoutTracker.Application.Features.Workout.Commands;

namespace WorkoutTracker.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class WorkoutsController : ControllerBase
{
    private readonly IMediator _mediator;

    public WorkoutsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> CreateWorkout([FromBody] CreateWorkoutCommand command)
    {
        var result = await _mediator.Send(command);
        return Ok(result);
    }
}
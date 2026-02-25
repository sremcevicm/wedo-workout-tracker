using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WorkoutTracker.Application.Features.Workouts.Commands.CreateWorkout;
using WorkoutTracker.Application.Features.Workouts.Commands.DeleteWorkout;
using WorkoutTracker.Application.Features.Workouts.Commands.UpdateWorkout;
using WorkoutTracker.Application.Features.Workouts.Queries.GetUserWorkouts;

namespace WorkoutTracker.API.Controllers;

[Authorize]
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

    [HttpGet]
    public async Task<IActionResult> GetWorkouts()
    {
        var result = await _mediator.Send(new GetUserWorkoutsQuery());
        return Ok(result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateWorkout(Guid id, [FromBody] UpdateWorkoutCommand command)
    {
        await _mediator.Send(command with { Id = id });
        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteWorkout(Guid id)
    {
        await _mediator.Send(new DeleteWorkoutCommand(id));
        return NoContent();
    }
}
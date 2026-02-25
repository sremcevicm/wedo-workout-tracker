using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WorkoutTracker.Application.Features.Workouts.Queries.GetMonthlyProgress;

namespace WorkoutTracker.API.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class ProgressController : ControllerBase
{
    private readonly IMediator _mediator;

    public ProgressController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{year}/{month}")]
    public async Task<IActionResult> GetMonthlyProgress(int year, int month)
    {
        var result = await _mediator.Send(new GetMonthlyProgressQuery(year, month));
        return Ok(result);
    }
}
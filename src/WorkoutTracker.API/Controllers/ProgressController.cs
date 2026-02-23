using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WorkoutTracker.Application.Features.Workout.Queries;

namespace WorkoutTracker.API.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class ProgressController : ControllerBase
{
    private Guid GetUserId() => Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
    private readonly IMediator _mediator;

    public ProgressController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{year}/{month}")]
    public async Task<IActionResult> GetMonthlyProgress(int year, int month)
    {
        var result = await _mediator.Send(new GetMonthlyProgressQuery(GetUserId(), year, month));
        return Ok(result);
    }
}
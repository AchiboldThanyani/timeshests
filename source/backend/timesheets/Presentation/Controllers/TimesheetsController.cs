using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using timesheets.Application.DTOs;
using timesheets.Application.DTOs.Requests;
using timesheets.Application.DTOs.Responses;
using timesheets.Application.Commands.Timesheets;
using timesheets.Application.Queries.Timesheets;
using timesheets.Domain.Shared;

namespace timesheets.Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TimesheetsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public TimesheetsController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    /// <summary>
    /// Get all timesheets
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<TimesheetDto>>> GetTimesheets()
    {
        var timesheets = await _mediator.Send(new GetAllTimesheetsQuery());
        return Ok(timesheets);
    }

    /// <summary>
    /// Get a specific timesheet by ID
    /// </summary>
    [HttpGet("{id}")]
    public async Task<ActionResult<TimesheetDto>> GetTimesheet(int id)
    {
        var timesheet = await _mediator.Send(new GetTimesheetByIdQuery(id));
        if (timesheet == null)
        {
            return NotFound();
        }
        return Ok(timesheet);
    }

    /// <summary>
    /// Create a new timesheet
    /// </summary>
    [HttpPost]
    public async Task<ActionResult<TimesheetDto>> CreateTimesheet(CreateTimesheetRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var command = _mapper.Map<CreateTimesheetCommand>(request);
        var result = await _mediator.Send(command);

        if (result.IsFailure)
        {
            return BadRequest(result.Error.Message);
        }

        return CreatedAtAction(nameof(GetTimesheet), new { id = result.Value.Id }, result.Value);
    }

    /// <summary>
    /// Update an existing timesheet
    /// </summary>
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateTimesheet(int id, UpdateTimesheetRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var command = _mapper.Map<UpdateTimesheetCommand>(request);
        command = command with { Id = id };
        var result = await _mediator.Send(command);

        if (result.IsFailure)
        {
            return result.Error.Code.Contains("NotFound")
                ? NotFound(result.Error.Message)
                : BadRequest(result.Error.Message);
        }

        return NoContent();
    }

    /// <summary>
    /// Delete a timesheet
    /// </summary>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTimesheet(int id)
    {
        var result = await _mediator.Send(new DeleteTimesheetCommand(id));

        if (result.IsFailure)
        {
            return result.Error.Code.Contains("NotFound")
                ? NotFound(result.Error.Message)
                : BadRequest(result.Error.Message);
        }

        return NoContent();
    }
}

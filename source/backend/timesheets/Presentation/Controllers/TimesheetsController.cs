using Microsoft.AspNetCore.Mvc;
using MediatR;
using timesheets.Application.DTOs;
using timesheets.Application.DTOs.Requests;
using timesheets.Application.DTOs.Responses;
using timesheets.Application.Commands.Timesheets;
using timesheets.Application.Queries.Timesheets;
using timesheets.Application.Mappers;

namespace timesheets.Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TimesheetsController : ControllerBase
{
    private readonly IMediator _mediator;

    public TimesheetsController(IMediator mediator)
    {
        _mediator = mediator;
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

        try
        {
            var command = TimesheetMapper.ToCommand(request);
            var createdTimesheet = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetTimesheet), new { id = createdTimesheet.Id }, createdTimesheet);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
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

        try
        {
            var command = TimesheetMapper.ToCommand(id, request);
            await _mediator.Send(command);
            return NoContent();
        }
        catch (ArgumentException ex)
        {
            return NotFound(ex.Message);
        }
    }

    /// <summary>
    /// Delete a timesheet
    /// </summary>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTimesheet(int id)
    {
        var result = await _mediator.Send(new DeleteTimesheetCommand(id));
        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }
}

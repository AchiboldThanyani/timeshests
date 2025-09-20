using Microsoft.AspNetCore.Mvc;
using timesheets.Application.DTOs;
using timesheets.Application.Services;

namespace timesheets.Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TimesheetsController : ControllerBase
{
    private readonly ITimesheetService _timesheetService;
    private readonly IProjectService _projectService;

    public TimesheetsController(ITimesheetService timesheetService, IProjectService projectService)
    {
        _timesheetService = timesheetService;
        _projectService = projectService;
    }

    /// <summary>
    /// Get all timesheets
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<TimesheetDto>>> GetTimesheets()
    {
        var timesheets = await _timesheetService.GetAllTimesheetsAsync();
        return Ok(timesheets);
    }

    /// <summary>
    /// Get a specific timesheet by ID
    /// </summary>
    [HttpGet("{id}")]
    public async Task<ActionResult<TimesheetDto>> GetTimesheet(int id)
    {
        var timesheet = await _timesheetService.GetTimesheetByIdAsync(id);
        if (timesheet == null)
        {
            return NotFound();
        }
        return Ok(timesheet);
    }

    /// <summary>
    /// Get timesheets by project ID
    /// </summary>
    [HttpGet("project/{projectId}")]
    public async Task<ActionResult<IEnumerable<TimesheetDto>>> GetTimesheetsByProject(int projectId)
    {
        var timesheets = await _timesheetService.GetTimesheetsByProjectAsync(projectId);
        return Ok(timesheets);
    }

    /// <summary>
    /// Get timesheets by employee name
    /// </summary>
    [HttpGet("employee/{employeeName}")]
    public async Task<ActionResult<IEnumerable<TimesheetDto>>> GetTimesheetsByEmployee(string employeeName)
    {
        var timesheets = await _timesheetService.GetTimesheetsByEmployeeAsync(employeeName);
        return Ok(timesheets);
    }

    /// <summary>
    /// Get timesheets by date range
    /// </summary>
    [HttpGet("daterange")]
    public async Task<ActionResult<IEnumerable<TimesheetDto>>> GetTimesheetsByDateRange(
        [FromQuery] DateTime startDate, 
        [FromQuery] DateTime endDate)
    {
        var timesheets = await _timesheetService.GetTimesheetsByDateRangeAsync(startDate, endDate);
        return Ok(timesheets);
    }

    /// <summary>
    /// Create a new timesheet
    /// </summary>
    [HttpPost]
    public async Task<ActionResult<TimesheetDto>> CreateTimesheet(TimesheetDto timesheetDto)
    {
        var createdTimesheet = await _timesheetService.CreateTimesheetAsync(timesheetDto);
        return CreatedAtAction(nameof(GetTimesheet), new { id = createdTimesheet.Id }, createdTimesheet);
    }

    /// <summary>
    /// Update an existing timesheet
    /// </summary>
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateTimesheet(int id, TimesheetDto timesheetDto)
    {
        if (id != timesheetDto.Id)
        {
            return BadRequest();
        }

        try
        {
            await _timesheetService.UpdateTimesheetAsync(timesheetDto);
        }
        catch
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Delete a timesheet
    /// </summary>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTimesheet(int id)
    {
        try
        {
            await _timesheetService.DeleteTimesheetAsync(id);
        }
        catch
        {
            return NotFound();
        }

        return NoContent();
    }
}

using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using timesheets.Application.DTOs;
using timesheets.Application.DTOs.Requests;
using timesheets.Application.DTOs.Responses;
using timesheets.Application.Commands.Projects;
using timesheets.Application.Queries.Projects;
using timesheets.Domain.Shared;

namespace timesheets.Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProjectsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public ProjectsController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    /// <summary>
    /// Get all projects
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProjectDto>>> GetProjects()
    {
        var projects = await _mediator.Send(new GetAllProjectsQuery());
        return Ok(projects);
    }

    /// <summary>
    /// Get a specific project by ID
    /// </summary>
    [HttpGet("{id}")]
    public async Task<ActionResult<ProjectDto>> GetProject(int id)
    {
        var project = await _mediator.Send(new GetProjectByIdQuery(id));
        if (project == null)
        {
            return NotFound();
        }
        return Ok(project);
    }

    /// <summary>
    /// Create a new project
    /// </summary>
    [HttpPost]
    public async Task<ActionResult<ProjectDto>> CreateProject(CreateProjectRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var command = _mapper.Map<CreateProjectCommand>(request);
        var result = await _mediator.Send(command);

        if (result.IsFailure)
        {
            return BadRequest(result.Error.Message);
        }

        return CreatedAtAction(nameof(GetProject), new { id = result.Value.Id }, result.Value);
    }

    /// <summary>
    /// Update an existing project
    /// </summary>
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateProject(int id, UpdateProjectRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var command = _mapper.Map<UpdateProjectCommand>(request);
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
    /// Delete a project
    /// </summary>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProject(int id)
    {
        var result = await _mediator.Send(new DeleteProjectCommand(id));

        if (result.IsFailure)
        {
            return result.Error.Code.Contains("NotFound")
                ? NotFound(result.Error.Message)
                : BadRequest(result.Error.Message);
        }

        return NoContent();
    }
}

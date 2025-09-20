using MediatR;
using timesheets.Application.DTOs;

namespace timesheets.Application.Commands.Projects;

public record UpdateProjectCommand(
    int Id,
    string Name,
    string? Description,
    string? Client,
    DateTime? StartDate,
    DateTime? EndDate
) : IRequest<ProjectDto>;
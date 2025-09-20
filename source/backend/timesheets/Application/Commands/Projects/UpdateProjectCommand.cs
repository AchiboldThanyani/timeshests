using MediatR;
using timesheets.Application.DTOs;
using timesheets.Domain.Shared;

namespace timesheets.Application.Commands.Projects;

public record UpdateProjectCommand(
    int Id,
    string Name,
    string? Description,
    string? Client,
    DateTime? StartDate,
    DateTime? EndDate
) : IRequest<Result<ProjectDto>>;
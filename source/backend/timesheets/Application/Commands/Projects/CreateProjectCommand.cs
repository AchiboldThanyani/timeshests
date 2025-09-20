using MediatR;
using timesheets.Application.DTOs;
using timesheets.Domain.Shared;

namespace timesheets.Application.Commands.Projects;

public record CreateProjectCommand(
    string Name,
    string? Description,
    string? Client,
    DateTime? StartDate,
    DateTime? EndDate
) : IRequest<Result<ProjectDto>>;
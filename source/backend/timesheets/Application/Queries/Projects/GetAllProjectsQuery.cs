using MediatR;
using timesheets.Application.DTOs;

namespace timesheets.Application.Queries.Projects;

public record GetAllProjectsQuery : IRequest<IEnumerable<ProjectDto>>;
using MediatR;
using timesheets.Application.DTOs;

namespace timesheets.Application.Queries.Projects;

public record GetProjectByIdQuery(int Id) : IRequest<ProjectDto?>;
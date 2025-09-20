using MediatR;
using timesheets.Application.Queries.Projects;
using timesheets.Application.DTOs;
using timesheets.Domain.Interfaces;

namespace timesheets.Application.Handlers.Projects;

public class GetProjectByIdHandler : IRequestHandler<GetProjectByIdQuery, ProjectDto?>
{
    private readonly IProjectRepository _projectRepository;

    public GetProjectByIdHandler(IProjectRepository projectRepository)
    {
        _projectRepository = projectRepository;
    }

    public async Task<ProjectDto?> Handle(GetProjectByIdQuery request, CancellationToken cancellationToken)
    {
        var project = await _projectRepository.GetByIdAsync(request.Id);
        if (project == null)
            return null;

        return new ProjectDto
        {
            Id = project.Id,
            Name = project.Name,
            Description = project.Description,
            StartDate = project.StartDate,
            EndDate = project.EndDate,
            CreatedDate = project.CreatedDate,
            UpdatedDate = project.UpdatedDate
        };
    }
}
using MediatR;
using timesheets.Application.Queries.Projects;
using timesheets.Application.DTOs;
using timesheets.Domain.Interfaces;

namespace timesheets.Application.Handlers.Projects;

public class GetAllProjectsHandler : IRequestHandler<GetAllProjectsQuery, IEnumerable<ProjectDto>>
{
    private readonly IProjectRepository _projectRepository;

    public GetAllProjectsHandler(IProjectRepository projectRepository)
    {
        _projectRepository = projectRepository;
    }

    public async Task<IEnumerable<ProjectDto>> Handle(GetAllProjectsQuery request, CancellationToken cancellationToken)
    {
        var projects = await _projectRepository.GetAllAsync();

        return projects.Select(project => new ProjectDto
        {
            Id = project.Id,
            Name = project.Name,
            Description = project.Description,
            StartDate = project.StartDate,
            EndDate = project.EndDate,
            CreatedDate = project.CreatedDate,
            UpdatedDate = project.UpdatedDate
        });
    }
}
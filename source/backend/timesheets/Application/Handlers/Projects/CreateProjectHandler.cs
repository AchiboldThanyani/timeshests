using MediatR;
using timesheets.Application.Commands.Projects;
using timesheets.Application.DTOs;
using timesheets.Application.Mappers;
using timesheets.Domain.Interfaces;

namespace timesheets.Application.Handlers.Projects;

public class CreateProjectHandler : IRequestHandler<CreateProjectCommand, ProjectDto>
{
    private readonly IProjectRepository _projectRepository;

    public CreateProjectHandler(IProjectRepository projectRepository)
    {
        _projectRepository = projectRepository;
    }

    public async Task<ProjectDto> Handle(CreateProjectCommand request, CancellationToken cancellationToken)
    {
        var project = ProjectMapper.ToEntity(request);
        var createdProject = await _projectRepository.AddAsync(project);

        return new ProjectDto
        {
            Id = createdProject.Id,
            Name = createdProject.Name,
            Description = createdProject.Description,
            StartDate = createdProject.StartDate,
            EndDate = createdProject.EndDate,
            CreatedDate = createdProject.CreatedDate,
            UpdatedDate = createdProject.UpdatedDate
        };
    }
}
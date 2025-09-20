using MediatR;
using timesheets.Application.Commands.Projects;
using timesheets.Application.DTOs;
using timesheets.Domain.Interfaces;

namespace timesheets.Application.Handlers.Projects;

public class UpdateProjectHandler : IRequestHandler<UpdateProjectCommand, ProjectDto>
{
    private readonly IProjectRepository _projectRepository;

    public UpdateProjectHandler(IProjectRepository projectRepository)
    {
        _projectRepository = projectRepository;
    }

    public async Task<ProjectDto> Handle(UpdateProjectCommand request, CancellationToken cancellationToken)
    {
        var existingProject = await _projectRepository.GetByIdAsync(request.Id);
        if (existingProject == null)
            throw new ArgumentException($"Project with ID {request.Id} not found.");

        existingProject.UpdateDetails(
            request.Name,
            request.Description,
            request.StartDate,
            request.EndDate,
            request.Client
        );

        var updatedProject = await _projectRepository.UpdateAsync(existingProject);

        return new ProjectDto
        {
            Id = updatedProject.Id,
            Name = updatedProject.Name,
            Description = updatedProject.Description,
            StartDate = updatedProject.StartDate,
            EndDate = updatedProject.EndDate,
            CreatedDate = updatedProject.CreatedDate,
            UpdatedDate = updatedProject.UpdatedDate
        };
    }
}
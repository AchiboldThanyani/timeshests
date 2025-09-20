using MediatR;
using timesheets.Application.Commands.Projects;
using timesheets.Application.DTOs;
using timesheets.Domain.Interfaces;
using timesheets.Domain.Shared;
using timesheets.Domain.Errors;

namespace timesheets.Application.Handlers.Projects;

public class UpdateProjectHandler : IRequestHandler<UpdateProjectCommand, Result<ProjectDto>>
{
    private readonly IProjectRepository _projectRepository;

    public UpdateProjectHandler(IProjectRepository projectRepository)
    {
        _projectRepository = projectRepository;
    }

    public async Task<Result<ProjectDto>> Handle(UpdateProjectCommand request, CancellationToken cancellationToken)
    {
        var existingProject = await _projectRepository.GetByIdAsync(request.Id);
        if (existingProject == null)
            return Result.Failure<ProjectDto>(ProjectError.NotFound);

        var updateResult = existingProject.UpdateDetails(
            request.Name,
            request.Description,
            request.StartDate,
            request.EndDate,
            request.Client
        );

        if (updateResult.IsFailure)
            return Result.Failure<ProjectDto>(updateResult.Error!);

        var updatedProject = await _projectRepository.UpdateAsync(existingProject);

        return new ProjectDto
        {
            Id = updatedProject.Id,
            Name = updatedProject.Name,
            Description = updatedProject.Description,
            Client = updatedProject.Client,
            StartDate = updatedProject.StartDate,
            EndDate = updatedProject.EndDate,
            IsActive = updatedProject.IsActive,
            CreatedDate = updatedProject.CreatedDate,
            UpdatedDate = updatedProject.UpdatedDate
        };
    }
}
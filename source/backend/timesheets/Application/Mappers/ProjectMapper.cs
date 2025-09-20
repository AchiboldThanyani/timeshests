using timesheets.Application.Commands.Projects;
using timesheets.Application.DTOs.Requests;
using timesheets.Application.DTOs.Responses;
using timesheets.Domain.Entities;

namespace timesheets.Application.Mappers;

public static class ProjectMapper
{
    public static CreateProjectCommand ToCommand(CreateProjectRequest request)
    {
        return new CreateProjectCommand(
            request.Name,
            request.Description,
            request.Client,
            request.StartDate,
            request.EndDate
        );
    }

    public static UpdateProjectCommand ToCommand(int id, UpdateProjectRequest request)
    {
        return new UpdateProjectCommand(
            id,
            request.Name,
            request.Description,
            request.Client,
            request.StartDate,
            request.EndDate
        );
    }

    public static Project ToEntity(CreateProjectCommand command)
    {
        return new Project(
            command.Name,
            command.Description,
            command.StartDate,
            command.EndDate,
            command.Client
        );
    }

    public static ProjectResponse ToResponse(Project project)
    {
        return new ProjectResponse
        {
            Id = project.Id,
            Name = project.Name,
            Description = project.Description,
            Client = project.Client,
            StartDate = project.StartDate,
            EndDate = project.EndDate,
            CreatedDate = project.CreatedDate,
            UpdatedDate = project.UpdatedDate
        };
    }

    public static IEnumerable<ProjectResponse> ToResponse(IEnumerable<Project> projects)
    {
        return projects.Select(ToResponse);
    }
}
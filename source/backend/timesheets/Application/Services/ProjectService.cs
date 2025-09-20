using timesheets.Application.DTOs;
using timesheets.Domain.Entities;
using timesheets.Domain.Interfaces;

namespace timesheets.Application.Services;

public interface IProjectService
{
    Task<IEnumerable<ProjectDto>> GetAllProjectsAsync();
    Task<ProjectDto?> GetProjectByIdAsync(int id);
    Task<ProjectDto> CreateProjectAsync(ProjectDto projectDto);
    Task<ProjectDto> UpdateProjectAsync(ProjectDto projectDto);
    Task DeleteProjectAsync(int id);
}

public class ProjectService : IProjectService
{
    private readonly IProjectRepository _projectRepository;

    public ProjectService(IProjectRepository projectRepository)
    {
        _projectRepository = projectRepository;
    }

    public async Task<IEnumerable<ProjectDto>> GetAllProjectsAsync()
    {
        var projects = await _projectRepository.GetAllAsync();
        return projects.Select(MapToDto);
    }

    public async Task<ProjectDto?> GetProjectByIdAsync(int id)
    {
        var project = await _projectRepository.GetByIdAsync(id);
        return project != null ? MapToDto(project) : null;
    }

    public async Task<ProjectDto> CreateProjectAsync(ProjectDto projectDto)
    {
        var project = MapToEntity(projectDto);
        var createdProject = await _projectRepository.AddAsync(project);
        return MapToDto(createdProject);
    }

    public async Task<ProjectDto> UpdateProjectAsync(ProjectDto projectDto)
    {
        var project = MapToEntity(projectDto);
        var updatedProject = await _projectRepository.UpdateAsync(project);
        return MapToDto(updatedProject);
    }

    public async Task DeleteProjectAsync(int id)
    {
        await _projectRepository.DeleteAsync(id);
    }

    private static ProjectDto MapToDto(Project project)
    {
        return new ProjectDto
        {
            Id = project.Id,
            Name = project.Name,
            Description = project.Description,
            Client = project.Client,
            StartDate = project.StartDate,
            EndDate = project.EndDate,
            IsActive = project.IsActive,
            CreatedDate = project.CreatedDate,
            UpdatedDate = project.UpdatedDate
        };
    }

    private static Project MapToEntity(ProjectDto dto)
    {
        return new Project
        {
            Id = dto.Id,
            Name = dto.Name,
            Description = dto.Description,
            Client = dto.Client,
            StartDate = dto.StartDate,
            EndDate = dto.EndDate,
            IsActive = dto.IsActive,
            CreatedDate = dto.CreatedDate,
            UpdatedDate = dto.UpdatedDate
        };
    }
}

using AutoMapper;
using timesheets.Application.Commands.Projects;
using timesheets.Application.DTOs;
using timesheets.Application.DTOs.Requests;
using timesheets.Application.DTOs.Responses;
using timesheets.Domain.Entities;

namespace timesheets.Application.Profiles;

public class ProjectProfile : Profile
{
    public ProjectProfile()
    {
        // Request DTO to Command mappings
        CreateMap<CreateProjectRequest, CreateProjectCommand>();
        CreateMap<UpdateProjectRequest, UpdateProjectCommand>()
            .ForMember(dest => dest.Id, opt => opt.Ignore()); // ID comes from route

        // Command to Entity mappings
        CreateMap<CreateProjectCommand, Project>()
            .ConstructUsing(src => new Project(
                src.Name,
                src.Description,
                src.StartDate,
                src.EndDate,
                src.Client));

        // Entity to DTO mappings (for backward compatibility)
        CreateMap<Project, ProjectDto>();

        // Entity to Response DTO mappings
        CreateMap<Project, ProjectResponse>();
    }
}
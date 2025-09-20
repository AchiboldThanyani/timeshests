using AutoMapper;
using timesheets.Application.Commands.Projects;
using timesheets.Application.DTOs.Requests;
using timesheets.Application.DTOs.Responses;
using timesheets.Domain.Entities;

namespace timesheets.Application.Mappers;

public class ProjectMapper
{
    private readonly IMapper _mapper;

    public ProjectMapper(IMapper mapper)
    {
        _mapper = mapper;
    }

    public CreateProjectCommand ToCommand(CreateProjectRequest request)
    {
        return _mapper.Map<CreateProjectCommand>(request);
    }

    public UpdateProjectCommand ToCommand(int id, UpdateProjectRequest request)
    {
        var command = _mapper.Map<UpdateProjectCommand>(request);
        return command with { Id = id };
    }

    public Project ToEntity(CreateProjectCommand command)
    {
        return _mapper.Map<Project>(command);
    }

    public ProjectResponse ToResponse(Project project)
    {
        return _mapper.Map<ProjectResponse>(project);
    }

    public IEnumerable<ProjectResponse> ToResponse(IEnumerable<Project> projects)
    {
        return _mapper.Map<IEnumerable<ProjectResponse>>(projects);
    }
}
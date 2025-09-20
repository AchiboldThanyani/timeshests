using AutoMapper;
using MediatR;
using timesheets.Application.Commands.Projects;
using timesheets.Application.DTOs;
using timesheets.Domain.Interfaces;
using timesheets.Domain.Entities;
using timesheets.Domain.Shared;

namespace timesheets.Application.Handlers.Projects;

public class CreateProjectHandler : IRequestHandler<CreateProjectCommand, Result<ProjectDto>>
{
    private readonly IProjectRepository _projectRepository;
    private readonly IMapper _mapper;

    public CreateProjectHandler(IProjectRepository projectRepository, IMapper mapper)
    {
        _projectRepository = projectRepository;
        _mapper = mapper;
    }

    public async Task<Result<ProjectDto>> Handle(CreateProjectCommand request, CancellationToken cancellationToken)
    {
        var projectResult = Project.Create(
            request.Name,
            request.Description,
            request.StartDate,
            request.EndDate,
            request.Client);

        if (projectResult.IsFailure)
            return Result.Failure<ProjectDto>(projectResult.Error!);

        var createdProject = await _projectRepository.AddAsync(projectResult.Value);
        return _mapper.Map<ProjectDto>(createdProject);
    }
}
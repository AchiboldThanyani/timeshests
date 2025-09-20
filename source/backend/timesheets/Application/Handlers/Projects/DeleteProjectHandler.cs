using MediatR;
using timesheets.Application.Commands.Projects;
using timesheets.Domain.Interfaces;
using timesheets.Domain.Shared;
using timesheets.Domain.Errors;

namespace timesheets.Application.Handlers.Projects;

public class DeleteProjectHandler : IRequestHandler<DeleteProjectCommand, Result>
{
    private readonly IProjectRepository _projectRepository;

    public DeleteProjectHandler(IProjectRepository projectRepository)
    {
        _projectRepository = projectRepository;
    }

    public async Task<Result> Handle(DeleteProjectCommand request, CancellationToken cancellationToken)
    {
        var project = await _projectRepository.GetByIdAsync(request.Id);
        if (project == null)
            return Result.Failure(ProjectError.NotFound);

        var deactivateResult = project.Deactivate();
        if (deactivateResult.IsFailure)
            return Result.Failure(deactivateResult.Error!);

        await _projectRepository.UpdateAsync(project);
        return Result.Success();
    }
}
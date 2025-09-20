using MediatR;
using timesheets.Application.Commands.Projects;
using timesheets.Domain.Interfaces;

namespace timesheets.Application.Handlers.Projects;

public class DeleteProjectHandler : IRequestHandler<DeleteProjectCommand, bool>
{
    private readonly IProjectRepository _projectRepository;

    public DeleteProjectHandler(IProjectRepository projectRepository)
    {
        _projectRepository = projectRepository;
    }

    public async Task<bool> Handle(DeleteProjectCommand request, CancellationToken cancellationToken)
    {
        var exists = await _projectRepository.ExistsAsync(request.Id);
        if (!exists)
            return false;

        await _projectRepository.DeleteAsync(request.Id);
        return true;
    }
}
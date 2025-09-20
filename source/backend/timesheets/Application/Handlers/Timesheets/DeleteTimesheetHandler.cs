using MediatR;
using timesheets.Application.Commands.Timesheets;
using timesheets.Domain.Interfaces;

namespace timesheets.Application.Handlers.Timesheets;

public class DeleteTimesheetHandler : IRequestHandler<DeleteTimesheetCommand, bool>
{
    private readonly ITimesheetRepository _timesheetRepository;

    public DeleteTimesheetHandler(ITimesheetRepository timesheetRepository)
    {
        _timesheetRepository = timesheetRepository;
    }

    public async Task<bool> Handle(DeleteTimesheetCommand request, CancellationToken cancellationToken)
    {
        var exists = await _timesheetRepository.ExistsAsync(request.Id);
        if (!exists)
            return false;

        await _timesheetRepository.DeleteAsync(request.Id);
        return true;
    }
}
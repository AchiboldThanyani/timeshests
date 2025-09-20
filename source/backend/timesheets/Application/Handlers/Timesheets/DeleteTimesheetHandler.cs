using MediatR;
using timesheets.Application.Commands.Timesheets;
using timesheets.Domain.Interfaces;
using timesheets.Domain.Shared;
using timesheets.Domain.Errors;

namespace timesheets.Application.Handlers.Timesheets;

public class DeleteTimesheetHandler : IRequestHandler<DeleteTimesheetCommand, Result>
{
    private readonly ITimesheetRepository _timesheetRepository;

    public DeleteTimesheetHandler(ITimesheetRepository timesheetRepository)
    {
        _timesheetRepository = timesheetRepository;
    }

    public async Task<Result> Handle(DeleteTimesheetCommand request, CancellationToken cancellationToken)
    {
        var timesheet = await _timesheetRepository.GetByIdAsync(request.Id);
        if (timesheet == null)
            return Result.Failure(TimesheetError.NotFound);

        if (!timesheet.CanBeModified())
            return Result.Failure(TimesheetError.CannotModifyOldEntry);

        await _timesheetRepository.DeleteAsync(request.Id);
        return Result.Success();
    }
}
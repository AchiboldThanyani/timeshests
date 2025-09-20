using MediatR;
using timesheets.Application.Commands.Timesheets;
using timesheets.Application.DTOs;
using timesheets.Domain.Interfaces;
using timesheets.Domain.Shared;
using timesheets.Domain.Errors;

namespace timesheets.Application.Handlers.Timesheets;

public class UpdateTimesheetHandler : IRequestHandler<UpdateTimesheetCommand, Result<TimesheetDto>>
{
    private readonly ITimesheetRepository _timesheetRepository;

    public UpdateTimesheetHandler(ITimesheetRepository timesheetRepository)
    {
        _timesheetRepository = timesheetRepository;
    }

    public async Task<Result<TimesheetDto>> Handle(UpdateTimesheetCommand request, CancellationToken cancellationToken)
    {
        var existingTimesheet = await _timesheetRepository.GetByIdAsync(request.Id);
        if (existingTimesheet == null)
            return Result.Failure<TimesheetDto>(TimesheetError.NotFound);

        var updateResult = existingTimesheet.UpdateTimeEntry(
            request.EmployeeName,
            request.ProjectId,
            request.Date,
            request.HoursWorked,
            request.Description
        );

        if (updateResult.IsFailure)
            return Result.Failure<TimesheetDto>(updateResult.Error!);

        var updatedTimesheet = await _timesheetRepository.UpdateAsync(existingTimesheet);

        return new TimesheetDto
        {
            Id = updatedTimesheet.Id,
            EmployeeName = updatedTimesheet.EmployeeName,
            ProjectId = updatedTimesheet.ProjectId,
            Description = updatedTimesheet.Description,
            Date = updatedTimesheet.Date,
            HoursWorked = updatedTimesheet.HoursWorked,
            CreatedDate = updatedTimesheet.CreatedDate,
            UpdatedDate = updatedTimesheet.UpdatedDate
        };
    }
}
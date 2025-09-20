using MediatR;
using timesheets.Application.Commands.Timesheets;
using timesheets.Application.DTOs;
using timesheets.Domain.Interfaces;

namespace timesheets.Application.Handlers.Timesheets;

public class UpdateTimesheetHandler : IRequestHandler<UpdateTimesheetCommand, TimesheetDto>
{
    private readonly ITimesheetRepository _timesheetRepository;

    public UpdateTimesheetHandler(ITimesheetRepository timesheetRepository)
    {
        _timesheetRepository = timesheetRepository;
    }

    public async Task<TimesheetDto> Handle(UpdateTimesheetCommand request, CancellationToken cancellationToken)
    {
        var existingTimesheet = await _timesheetRepository.GetByIdAsync(request.Id);
        if (existingTimesheet == null)
            throw new ArgumentException($"Timesheet with ID {request.Id} not found.");

        existingTimesheet.UpdateTimeEntry(
            request.EmployeeName,
            request.ProjectId,
            request.Date,
            request.HoursWorked,
            request.Description
        );

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
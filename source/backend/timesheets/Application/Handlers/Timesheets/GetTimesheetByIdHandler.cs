using MediatR;
using timesheets.Application.Queries.Timesheets;
using timesheets.Application.DTOs;
using timesheets.Domain.Interfaces;

namespace timesheets.Application.Handlers.Timesheets;

public class GetTimesheetByIdHandler : IRequestHandler<GetTimesheetByIdQuery, TimesheetDto?>
{
    private readonly ITimesheetRepository _timesheetRepository;

    public GetTimesheetByIdHandler(ITimesheetRepository timesheetRepository)
    {
        _timesheetRepository = timesheetRepository;
    }

    public async Task<TimesheetDto?> Handle(GetTimesheetByIdQuery request, CancellationToken cancellationToken)
    {
        var timesheet = await _timesheetRepository.GetByIdAsync(request.Id);
        if (timesheet == null)
            return null;

        return new TimesheetDto
        {
            Id = timesheet.Id,
            EmployeeName = timesheet.EmployeeName,
            ProjectId = timesheet.ProjectId,
            Description = timesheet.Description,
            Date = timesheet.Date,
            HoursWorked = timesheet.HoursWorked,
            CreatedDate = timesheet.CreatedDate,
            UpdatedDate = timesheet.UpdatedDate
        };
    }
}
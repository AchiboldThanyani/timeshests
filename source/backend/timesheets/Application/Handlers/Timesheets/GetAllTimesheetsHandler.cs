using MediatR;
using timesheets.Application.Queries.Timesheets;
using timesheets.Application.DTOs;
using timesheets.Domain.Interfaces;

namespace timesheets.Application.Handlers.Timesheets;

public class GetAllTimesheetsHandler : IRequestHandler<GetAllTimesheetsQuery, IEnumerable<TimesheetDto>>
{
    private readonly ITimesheetRepository _timesheetRepository;

    public GetAllTimesheetsHandler(ITimesheetRepository timesheetRepository)
    {
        _timesheetRepository = timesheetRepository;
    }

    public async Task<IEnumerable<TimesheetDto>> Handle(GetAllTimesheetsQuery request, CancellationToken cancellationToken)
    {
        var timesheets = await _timesheetRepository.GetAllAsync();

        return timesheets.Select(timesheet => new TimesheetDto
        {
            Id = timesheet.Id,
            EmployeeName = timesheet.EmployeeName,
            ProjectId = timesheet.ProjectId,
            Description = timesheet.Description,
            Date = timesheet.Date,
            HoursWorked = timesheet.HoursWorked,
            CreatedDate = timesheet.CreatedDate,
            UpdatedDate = timesheet.UpdatedDate
        });
    }
}
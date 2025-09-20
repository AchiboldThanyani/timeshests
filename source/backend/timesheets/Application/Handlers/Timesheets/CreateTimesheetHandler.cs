using MediatR;
using timesheets.Application.Commands.Timesheets;
using timesheets.Application.DTOs;
using timesheets.Application.Mappers;
using timesheets.Domain.Interfaces;

namespace timesheets.Application.Handlers.Timesheets;

public class CreateTimesheetHandler : IRequestHandler<CreateTimesheetCommand, TimesheetDto>
{
    private readonly ITimesheetRepository _timesheetRepository;

    public CreateTimesheetHandler(ITimesheetRepository timesheetRepository)
    {
        _timesheetRepository = timesheetRepository;
    }

    public async Task<TimesheetDto> Handle(CreateTimesheetCommand request, CancellationToken cancellationToken)
    {
        var timesheet = TimesheetMapper.ToEntity(request);
        var createdTimesheet = await _timesheetRepository.AddAsync(timesheet);

        return new TimesheetDto
        {
            Id = createdTimesheet.Id,
            EmployeeName = createdTimesheet.EmployeeName,
            ProjectId = createdTimesheet.ProjectId,
            Description = createdTimesheet.Description,
            Date = createdTimesheet.Date,
            HoursWorked = createdTimesheet.HoursWorked,
            CreatedDate = createdTimesheet.CreatedDate,
            UpdatedDate = createdTimesheet.UpdatedDate
        };
    }
}
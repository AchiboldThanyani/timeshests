using timesheets.Application.Commands.Timesheets;
using timesheets.Application.DTOs.Requests;
using timesheets.Application.DTOs.Responses;
using timesheets.Domain.Entities;

namespace timesheets.Application.Mappers;

public static class TimesheetMapper
{
    public static CreateTimesheetCommand ToCommand(CreateTimesheetRequest request)
    {
        return new CreateTimesheetCommand(
            request.EmployeeName,
            request.ProjectId,
            request.Description,
            request.Date,
            request.HoursWorked
        );
    }

    public static UpdateTimesheetCommand ToCommand(int id, UpdateTimesheetRequest request)
    {
        return new UpdateTimesheetCommand(
            id,
            request.EmployeeName,
            request.ProjectId,
            request.Description,
            request.Date,
            request.HoursWorked
        );
    }

    public static Timesheet ToEntity(CreateTimesheetCommand command)
    {
        return new Timesheet(
            command.EmployeeName,
            command.ProjectId,
            command.Date,
            command.HoursWorked,
            command.Description
        );
    }

    public static TimesheetResponse ToResponse(Timesheet timesheet)
    {
        return new TimesheetResponse
        {
            Id = timesheet.Id,
            EmployeeName = timesheet.EmployeeName,
            ProjectId = timesheet.ProjectId,
            ProjectName = timesheet.Project?.Name,
            Description = timesheet.Description,
            Date = timesheet.Date,
            HoursWorked = timesheet.HoursWorked,
            CreatedDate = timesheet.CreatedDate,
            UpdatedDate = timesheet.UpdatedDate
        };
    }

    public static IEnumerable<TimesheetResponse> ToResponse(IEnumerable<Timesheet> timesheets)
    {
        return timesheets.Select(ToResponse);
    }
}
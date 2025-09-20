using MediatR;
using timesheets.Application.DTOs;

namespace timesheets.Application.Commands.Timesheets;

public record CreateTimesheetCommand(
    string EmployeeName,
    int ProjectId,
    string? Description,
    DateTime Date,
    decimal HoursWorked
) : IRequest<TimesheetDto>;
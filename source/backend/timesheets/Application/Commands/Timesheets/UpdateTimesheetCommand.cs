using MediatR;
using timesheets.Application.DTOs;

namespace timesheets.Application.Commands.Timesheets;

public record UpdateTimesheetCommand(
    int Id,
    string EmployeeName,
    int ProjectId,
    string? Description,
    DateTime Date,
    decimal HoursWorked
) : IRequest<TimesheetDto>;
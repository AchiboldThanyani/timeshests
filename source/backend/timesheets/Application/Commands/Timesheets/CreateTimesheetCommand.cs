using MediatR;
using timesheets.Application.DTOs;
using timesheets.Domain.Shared;

namespace timesheets.Application.Commands.Timesheets;

public record CreateTimesheetCommand(
    string EmployeeName,
    int ProjectId,
    string? Description,
    DateTime Date,
    decimal HoursWorked
) : IRequest<Result<TimesheetDto>>;
using MediatR;

namespace timesheets.Application.Commands.Timesheets;

public record DeleteTimesheetCommand(int Id) : IRequest<bool>;
using MediatR;
using timesheets.Domain.Shared;

namespace timesheets.Application.Commands.Timesheets;

public record DeleteTimesheetCommand(int Id) : IRequest<Result>;
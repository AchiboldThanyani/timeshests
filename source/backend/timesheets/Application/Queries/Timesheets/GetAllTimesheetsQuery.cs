using MediatR;
using timesheets.Application.DTOs;

namespace timesheets.Application.Queries.Timesheets;

public record GetAllTimesheetsQuery : IRequest<IEnumerable<TimesheetDto>>;
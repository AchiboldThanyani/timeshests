using MediatR;
using timesheets.Application.DTOs;

namespace timesheets.Application.Queries.Timesheets;

public record GetTimesheetByIdQuery(int Id) : IRequest<TimesheetDto?>;
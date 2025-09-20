using AutoMapper;
using timesheets.Application.Commands.Timesheets;
using timesheets.Application.DTOs.Requests;
using timesheets.Application.DTOs.Responses;
using timesheets.Domain.Entities;

namespace timesheets.Application.Mappers;

public class TimesheetMapper
{
    private readonly IMapper _mapper;

    public TimesheetMapper(IMapper mapper)
    {
        _mapper = mapper;
    }

    public CreateTimesheetCommand ToCommand(CreateTimesheetRequest request)
    {
        return _mapper.Map<CreateTimesheetCommand>(request);
    }

    public UpdateTimesheetCommand ToCommand(int id, UpdateTimesheetRequest request)
    {
        var command = _mapper.Map<UpdateTimesheetCommand>(request);
        return command with { Id = id };
    }

    public Timesheet ToEntity(CreateTimesheetCommand command)
    {
        return _mapper.Map<Timesheet>(command);
    }

    public TimesheetResponse ToResponse(Timesheet timesheet)
    {
        return _mapper.Map<TimesheetResponse>(timesheet);
    }

    public IEnumerable<TimesheetResponse> ToResponse(IEnumerable<Timesheet> timesheets)
    {
        return _mapper.Map<IEnumerable<TimesheetResponse>>(timesheets);
    }
}
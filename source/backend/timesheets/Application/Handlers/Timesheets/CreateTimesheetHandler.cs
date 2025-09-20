using AutoMapper;
using MediatR;
using timesheets.Application.Commands.Timesheets;
using timesheets.Application.DTOs;
using timesheets.Domain.Interfaces;
using timesheets.Domain.Entities;
using timesheets.Domain.Shared;

namespace timesheets.Application.Handlers.Timesheets;

public class CreateTimesheetHandler : IRequestHandler<CreateTimesheetCommand, Result<TimesheetDto>>
{
    private readonly ITimesheetRepository _timesheetRepository;
    private readonly IMapper _mapper;

    public CreateTimesheetHandler(ITimesheetRepository timesheetRepository, IMapper mapper)
    {
        _timesheetRepository = timesheetRepository;
        _mapper = mapper;
    }

    public async Task<Result<TimesheetDto>> Handle(CreateTimesheetCommand request, CancellationToken cancellationToken)
    {
        var timesheetResult = Timesheet.Create(
            request.EmployeeName,
            request.ProjectId,
            request.Date,
            request.HoursWorked,
            request.Description);

        if (timesheetResult.IsFailure)
            return Result.Failure<TimesheetDto>(timesheetResult.Error!);

        var createdTimesheet = await _timesheetRepository.AddAsync(timesheetResult.Value);
        return _mapper.Map<TimesheetDto>(createdTimesheet);
    }
}
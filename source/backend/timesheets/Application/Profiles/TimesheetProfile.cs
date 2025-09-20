using AutoMapper;
using timesheets.Application.Commands.Timesheets;
using timesheets.Application.DTOs;
using timesheets.Application.DTOs.Requests;
using timesheets.Application.DTOs.Responses;
using timesheets.Domain.Entities;

namespace timesheets.Application.Profiles;

public class TimesheetProfile : Profile
{
    public TimesheetProfile()
    {
        // Request DTO to Command mappings
        CreateMap<CreateTimesheetRequest, CreateTimesheetCommand>();
        CreateMap<UpdateTimesheetRequest, UpdateTimesheetCommand>()
            .ForMember(dest => dest.Id, opt => opt.Ignore()); // ID comes from route

        // Command to Entity mappings
        CreateMap<CreateTimesheetCommand, Timesheet>()
            .ConstructUsing(src => new Timesheet(
                src.EmployeeName,
                src.ProjectId,
                src.Date,
                src.HoursWorked,
                src.Description));

        // Entity to DTO mappings (for backward compatibility)
        CreateMap<Timesheet, TimesheetDto>();

        // Entity to Response DTO mappings
        CreateMap<Timesheet, TimesheetResponse>()
            .ForMember(dest => dest.ProjectName, opt => opt.MapFrom(src => src.Project != null ? src.Project.Name : null));
    }
}
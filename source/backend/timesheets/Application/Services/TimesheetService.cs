using timesheets.Application.DTOs;
using timesheets.Domain.Entities;
using timesheets.Domain.Interfaces;

namespace timesheets.Application.Services;

public interface ITimesheetService
{
    Task<IEnumerable<TimesheetDto>> GetAllTimesheetsAsync();
    Task<TimesheetDto?> GetTimesheetByIdAsync(int id);
    Task<IEnumerable<TimesheetDto>> GetTimesheetsByProjectAsync(int projectId);
    Task<IEnumerable<TimesheetDto>> GetTimesheetsByEmployeeAsync(string employeeName);
    Task<IEnumerable<TimesheetDto>> GetTimesheetsByDateRangeAsync(DateTime startDate, DateTime endDate);
    Task<TimesheetDto> CreateTimesheetAsync(TimesheetDto timesheetDto);
    Task<TimesheetDto> UpdateTimesheetAsync(TimesheetDto timesheetDto);
    Task DeleteTimesheetAsync(int id);
}

public class TimesheetService : ITimesheetService
{
    private readonly ITimesheetRepository _timesheetRepository;
    private readonly IProjectRepository _projectRepository;

    public TimesheetService(ITimesheetRepository timesheetRepository, IProjectRepository projectRepository)
    {
        _timesheetRepository = timesheetRepository;
        _projectRepository = projectRepository;
    }

    public async Task<IEnumerable<TimesheetDto>> GetAllTimesheetsAsync()
    {
        var timesheets = await _timesheetRepository.GetAllAsync();
        return await MapToDtosAsync(timesheets);
    }

    public async Task<TimesheetDto?> GetTimesheetByIdAsync(int id)
    {
        var timesheet = await _timesheetRepository.GetByIdAsync(id);
        return timesheet != null ? await MapToDtoAsync(timesheet) : null;
    }

    public async Task<IEnumerable<TimesheetDto>> GetTimesheetsByProjectAsync(int projectId)
    {
        var timesheets = await _timesheetRepository.GetByProjectIdAsync(projectId);
        return await MapToDtosAsync(timesheets);
    }

    public async Task<IEnumerable<TimesheetDto>> GetTimesheetsByEmployeeAsync(string employeeName)
    {
        var timesheets = await _timesheetRepository.GetByEmployeeAsync(employeeName);
        return await MapToDtosAsync(timesheets);
    }

    public async Task<IEnumerable<TimesheetDto>> GetTimesheetsByDateRangeAsync(DateTime startDate, DateTime endDate)
    {
        var timesheets = await _timesheetRepository.GetByDateRangeAsync(startDate, endDate);
        return await MapToDtosAsync(timesheets);
    }

    public async Task<TimesheetDto> CreateTimesheetAsync(TimesheetDto timesheetDto)
    {
        var timesheet = MapToEntity(timesheetDto);
        var createdTimesheet = await _timesheetRepository.AddAsync(timesheet);
        return await MapToDtoAsync(createdTimesheet);
    }

    public async Task<TimesheetDto> UpdateTimesheetAsync(TimesheetDto timesheetDto)
    {
        var timesheet = MapToEntity(timesheetDto);
        var updatedTimesheet = await _timesheetRepository.UpdateAsync(timesheet);
        return await MapToDtoAsync(updatedTimesheet);
    }

    public async Task DeleteTimesheetAsync(int id)
    {
        await _timesheetRepository.DeleteAsync(id);
    }

    private async Task<IEnumerable<TimesheetDto>> MapToDtosAsync(IEnumerable<Timesheet> timesheets)
    {
        var dtos = new List<TimesheetDto>();
        foreach (var timesheet in timesheets)
        {
            dtos.Add(await MapToDtoAsync(timesheet));
        }
        return dtos;
    }

    private async Task<TimesheetDto> MapToDtoAsync(Timesheet timesheet)
    {
        var project = await _projectRepository.GetByIdAsync(timesheet.ProjectId);
        return new TimesheetDto
        {
            Id = timesheet.Id,
            EmployeeName = timesheet.EmployeeName,
            ProjectId = timesheet.ProjectId,
            ProjectName = project?.Name ?? "Unknown Project",
            Description = timesheet.Description,
            Date = timesheet.Date,
            HoursWorked = timesheet.HoursWorked,
            CreatedDate = timesheet.CreatedDate,
            UpdatedDate = timesheet.UpdatedDate
        };
    }

    private static Timesheet MapToEntity(TimesheetDto dto)
    {
        return new Timesheet
        {
            Id = dto.Id,
            EmployeeName = dto.EmployeeName,
            ProjectId = dto.ProjectId,
            Description = dto.Description,
            Date = dto.Date,
            HoursWorked = dto.HoursWorked,
            CreatedDate = dto.CreatedDate,
            UpdatedDate = dto.UpdatedDate
        };
    }
}

using timesheets.Domain.Entities;

namespace timesheets.Domain.Interfaces;

public interface ITimesheetRepository
{
    Task<IEnumerable<Timesheet>> GetAllAsync();
    Task<Timesheet?> GetByIdAsync(int id);
    Task<IEnumerable<Timesheet>> GetByProjectIdAsync(int projectId);
    Task<IEnumerable<Timesheet>> GetByEmployeeAsync(string employeeName);
    Task<IEnumerable<Timesheet>> GetByDateRangeAsync(DateTime startDate, DateTime endDate);
    Task<Timesheet> AddAsync(Timesheet timesheet);
    Task<Timesheet> UpdateAsync(Timesheet timesheet);
    Task DeleteAsync(int id);
    Task<bool> ExistsAsync(int id);
}

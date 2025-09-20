using Microsoft.EntityFrameworkCore;
using timesheets.Domain.Entities;
using timesheets.Domain.Interfaces;
using timesheets.Infrastructure.Data;

namespace timesheets.Infrastructure.Repositories;

public class TimesheetRepository : ITimesheetRepository
{
    private readonly TimesheetDbContext _context;

    public TimesheetRepository(TimesheetDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Timesheet>> GetAllAsync()
    {
        return await _context.Timesheets
            .Include(t => t.Project)
            .OrderByDescending(t => t.Date)
            .ToListAsync();
    }

    public async Task<Timesheet?> GetByIdAsync(int id)
    {
        return await _context.Timesheets
            .Include(t => t.Project)
            .FirstOrDefaultAsync(t => t.Id == id);
    }

    public async Task<IEnumerable<Timesheet>> GetByProjectIdAsync(int projectId)
    {
        return await _context.Timesheets
            .Include(t => t.Project)
            .Where(t => t.ProjectId == projectId)
            .OrderByDescending(t => t.Date)
            .ToListAsync();
    }

    public async Task<IEnumerable<Timesheet>> GetByEmployeeAsync(string employeeName)
    {
        return await _context.Timesheets
            .Include(t => t.Project)
            .Where(t => t.EmployeeName.ToLower().Contains(employeeName.ToLower()))
            .OrderByDescending(t => t.Date)
            .ToListAsync();
    }

    public async Task<IEnumerable<Timesheet>> GetByDateRangeAsync(DateTime startDate, DateTime endDate)
    {
        return await _context.Timesheets
            .Include(t => t.Project)
            .Where(t => t.Date >= startDate && t.Date <= endDate)
            .OrderByDescending(t => t.Date)
            .ToListAsync();
    }

    public async Task<Timesheet> AddAsync(Timesheet timesheet)
    {
        _context.Timesheets.Add(timesheet);
        await _context.SaveChangesAsync();
        return timesheet;
    }

    public async Task<Timesheet> UpdateAsync(Timesheet timesheet)
    {
        timesheet.UpdatedDate = DateTime.Now;
        _context.Timesheets.Update(timesheet);
        await _context.SaveChangesAsync();
        return timesheet;
    }

    public async Task DeleteAsync(int id)
    {
        var timesheet = await _context.Timesheets.FindAsync(id);
        if (timesheet != null)
        {
            _context.Timesheets.Remove(timesheet);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<bool> ExistsAsync(int id)
    {
        return await _context.Timesheets.AnyAsync(t => t.Id == id);
    }
}

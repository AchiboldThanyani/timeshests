using Microsoft.EntityFrameworkCore;
using timesheets.Domain.Entities;
using timesheets.Domain.Interfaces;
using timesheets.Infrastructure.Data;

namespace timesheets.Infrastructure.Repositories;

public class ProjectRepository : IProjectRepository
{
    private readonly TimesheetDbContext _context;

    public ProjectRepository(TimesheetDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Project>> GetAllAsync()
    {
        return await _context.Projects
            .OrderBy(p => p.Name)
            .ToListAsync();
    }

    public async Task<Project?> GetByIdAsync(int id)
    {
        return await _context.Projects
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<Project> AddAsync(Project project)
    {
        _context.Projects.Add(project);
        await _context.SaveChangesAsync();
        return project;
    }

    public async Task<Project> UpdateAsync(Project project)
    {
        project.UpdatedDate = DateTime.Now;
        _context.Projects.Update(project);
        await _context.SaveChangesAsync();
        return project;
    }

    public async Task DeleteAsync(int id)
    {
        var project = await _context.Projects.FindAsync(id);
        if (project != null)
        {
            _context.Projects.Remove(project);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<bool> ExistsAsync(int id)
    {
        return await _context.Projects.AnyAsync(p => p.Id == id);
    }
}

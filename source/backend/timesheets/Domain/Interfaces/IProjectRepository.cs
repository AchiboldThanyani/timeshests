using timesheets.Domain.Entities;

namespace timesheets.Domain.Interfaces;

public interface IProjectRepository
{
    Task<IEnumerable<Project>> GetAllAsync();
    Task<Project?> GetByIdAsync(int id);
    Task<Project> AddAsync(Project project);
    Task<Project> UpdateAsync(Project project);
    Task DeleteAsync(int id);
    Task<bool> ExistsAsync(int id);
}

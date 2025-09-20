using System.ComponentModel.DataAnnotations;

namespace timesheets.Domain.Entities;

public class Project
{
    public Project() { }

    public Project(string name, string? description, DateTime? startDate = null, DateTime? endDate = null, string? client = null)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Project name cannot be empty", nameof(name));

        if (startDate.HasValue && endDate.HasValue && endDate <= startDate)
            throw new ArgumentException("End date must be after start date", nameof(endDate));

        Name = name;
        Description = description;
        Client = client;
        StartDate = startDate;
        EndDate = endDate;
        CreatedDate = DateTime.UtcNow;
        IsActive = true;
    }

    public int Id { get; set; }

    [Required]
    [StringLength(200)]
    public string Name { get; set; } = string.Empty;

    [StringLength(1000)]
    public string? Description { get; set; }

    [StringLength(100)]
    public string? Client { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public bool IsActive { get; set; } = true;

    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

    public DateTime? UpdatedDate { get; set; }

    // Navigation property
    public virtual ICollection<Timesheet> Timesheets { get; set; } = new List<Timesheet>();

    // Rich domain methods
    public void UpdateDetails(string name, string? description, DateTime? startDate, DateTime? endDate, string? client = null)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Project name cannot be empty", nameof(name));

        if (startDate.HasValue && endDate.HasValue && endDate <= startDate)
            throw new ArgumentException("End date must be after start date", nameof(endDate));

        Name = name;
        Description = description;
        Client = client;
        StartDate = startDate;
        EndDate = endDate;
        UpdatedDate = DateTime.UtcNow;
    }

    public void Deactivate()
    {
        IsActive = false;
        UpdatedDate = DateTime.UtcNow;
    }

    public void Activate()
    {
        IsActive = true;
        UpdatedDate = DateTime.UtcNow;
    }

    public bool IsCurrentlyActive => IsActive && (EndDate == null || EndDate > DateTime.UtcNow);

    public int GetDurationInDays()
    {
        if (!StartDate.HasValue) return 0;
        var endDate = EndDate ?? DateTime.UtcNow;
        return (endDate - StartDate.Value).Days;
    }

    public decimal GetTotalHoursLogged()
    {
        return Timesheets.Sum(t => t.HoursWorked);
    }

    public bool CanLogTime()
    {
        return IsCurrentlyActive;
    }
}

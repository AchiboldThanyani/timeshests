using System.ComponentModel.DataAnnotations;

namespace timesheets.Domain.Entities;

public class Timesheet
{
    public Timesheet() { }

    public Timesheet(string employeeName, int projectId, DateTime date, decimal hoursWorked, string? description = null)
    {
        if (string.IsNullOrWhiteSpace(employeeName))
            throw new ArgumentException("Employee name cannot be empty", nameof(employeeName));

        if (projectId <= 0)
            throw new ArgumentException("Project ID must be positive", nameof(projectId));

        if (hoursWorked <= 0 || hoursWorked > 24)
            throw new ArgumentException("Hours worked must be between 0.1 and 24", nameof(hoursWorked));

        if (date > DateTime.Now.Date)
            throw new ArgumentException("Cannot log time for future dates", nameof(date));

        EmployeeName = employeeName;
        ProjectId = projectId;
        Date = date;
        HoursWorked = hoursWorked;
        Description = description;
        CreatedDate = DateTime.UtcNow;
    }

    public int Id { get; set; }

    [Required]
    [StringLength(100)]
    public string EmployeeName { get; set; } = string.Empty;

    [Required]
    public int ProjectId { get; set; }

    // Navigation property
    public virtual Project Project { get; set; } = null!;

    [StringLength(500)]
    public string? Description { get; set; }

    [Required]
    public DateTime Date { get; set; }

    [Required]
    [Range(0.1, 24)]
    public decimal HoursWorked { get; set; }

    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

    public DateTime? UpdatedDate { get; set; }

    // Rich domain methods
    public void UpdateTimeEntry(string employeeName, int projectId, DateTime date, decimal hoursWorked, string? description = null)
    {
        if (string.IsNullOrWhiteSpace(employeeName))
            throw new ArgumentException("Employee name cannot be empty", nameof(employeeName));

        if (projectId <= 0)
            throw new ArgumentException("Project ID must be positive", nameof(projectId));

        if (hoursWorked <= 0 || hoursWorked > 24)
            throw new ArgumentException("Hours worked must be between 0.1 and 24", nameof(hoursWorked));

        if (date > DateTime.Now.Date)
            throw new ArgumentException("Cannot log time for future dates", nameof(date));

        EmployeeName = employeeName;
        ProjectId = projectId;
        Date = date;
        HoursWorked = hoursWorked;
        Description = description;
        UpdatedDate = DateTime.UtcNow;
    }

    public void UpdateDescription(string? description)
    {
        Description = description;
        UpdatedDate = DateTime.UtcNow;
    }

    public void AdjustHours(decimal newHours)
    {
        if (newHours <= 0 || newHours > 24)
            throw new ArgumentException("Hours worked must be between 0.1 and 24", nameof(newHours));

        HoursWorked = newHours;
        UpdatedDate = DateTime.UtcNow;
    }

    public bool IsOvertimeEntry => HoursWorked > 8;

    public bool IsWeekendEntry => Date.DayOfWeek == DayOfWeek.Saturday || Date.DayOfWeek == DayOfWeek.Sunday;

    public bool IsRecentEntry => (DateTime.UtcNow - CreatedDate).TotalDays <= 7;

    public bool CanBeModified()
    {
        // Can only modify entries from the last 30 days
        return (DateTime.UtcNow - Date).TotalDays <= 30;
    }
}

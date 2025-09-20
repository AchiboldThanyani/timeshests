using System.ComponentModel.DataAnnotations;

namespace timesheets.Domain.Entities;

public class Timesheet
{
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

    public DateTime CreatedDate { get; set; } = DateTime.Now;

    public DateTime? UpdatedDate { get; set; }
}

using System.ComponentModel.DataAnnotations;

namespace timesheets.Application.DTOs.Requests;

public class CreateTimesheetRequest
{
    [Required]
    [StringLength(100, ErrorMessage = "Employee name cannot exceed 100 characters")]
    public string EmployeeName { get; set; } = string.Empty;

    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "ProjectId must be a positive number")]
    public int ProjectId { get; set; }

    [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters")]
    public string? Description { get; set; }

    [Required]
    public DateTime Date { get; set; }

    [Required]
    [Range(0.1, 24, ErrorMessage = "Hours worked must be between 0.1 and 24")]
    public decimal HoursWorked { get; set; }
}
using System.ComponentModel.DataAnnotations;

namespace timesheets.Application.DTOs;

public class TimesheetDto
{
    public int Id { get; set; }

    [Required]
    [StringLength(100)]
    [Display(Name = "Employee Name")]
    public string EmployeeName { get; set; } = string.Empty;

    [Required]
    [Display(Name = "Project")]
    public int ProjectId { get; set; }

    [Display(Name = "Project Name")]
    public string ProjectName { get; set; } = string.Empty;

    [StringLength(500)]
    [Display(Name = "Description")]
    public string? Description { get; set; }

    [Required]
    [Display(Name = "Date")]
    [DataType(DataType.Date)]
    public DateTime Date { get; set; }

    [Required]
    [Range(0.1, 24, ErrorMessage = "Hours must be between 0.1 and 24")]
    [Display(Name = "Hours Worked")]
    public decimal HoursWorked { get; set; }

    [Display(Name = "Created Date")]
    public DateTime CreatedDate { get; set; }

    [Display(Name = "Updated Date")]
    public DateTime? UpdatedDate { get; set; }
}

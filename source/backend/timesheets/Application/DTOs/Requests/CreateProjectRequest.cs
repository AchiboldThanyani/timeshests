using System.ComponentModel.DataAnnotations;

namespace timesheets.Application.DTOs.Requests;

public class CreateProjectRequest
{
    [Required]
    [StringLength(200, ErrorMessage = "Project name cannot exceed 200 characters")]
    public string Name { get; set; } = string.Empty;

    [StringLength(1000, ErrorMessage = "Description cannot exceed 1000 characters")]
    public string? Description { get; set; }

    [StringLength(100, ErrorMessage = "Client name cannot exceed 100 characters")]
    public string? Client { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public bool ValidateDateRange()
    {
        return !StartDate.HasValue || !EndDate.HasValue || EndDate > StartDate;
    }
}
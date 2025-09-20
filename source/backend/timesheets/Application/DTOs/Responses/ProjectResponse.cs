namespace timesheets.Application.DTOs.Responses;

public class ProjectResponse
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string? Client { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime? UpdatedDate { get; set; }
    public bool IsActive => EndDate == null || EndDate > DateTime.UtcNow;
    public int DurationInDays => StartDate.HasValue
        ? (EndDate?.Subtract(StartDate.Value).Days ?? DateTime.UtcNow.Subtract(StartDate.Value).Days)
        : 0;
}
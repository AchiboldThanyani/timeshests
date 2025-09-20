namespace timesheets.Application.DTOs.Responses;

public class TimesheetResponse
{
    public int Id { get; set; }
    public string EmployeeName { get; set; } = string.Empty;
    public int ProjectId { get; set; }
    public string? ProjectName { get; set; }
    public string? Description { get; set; }
    public DateTime Date { get; set; }
    public decimal HoursWorked { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime? UpdatedDate { get; set; }
}
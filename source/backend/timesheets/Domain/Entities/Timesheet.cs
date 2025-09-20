using System.ComponentModel.DataAnnotations;
using timesheets.Domain.Errors;
using timesheets.Domain.Shared;

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

        if (date > DateTime.UtcNow.Date)
            throw new ArgumentException("Cannot log time for future dates", nameof(date));

        EmployeeName = employeeName;
        ProjectId = projectId;
        Date = date;
        HoursWorked = hoursWorked;
        Description = description;
        CreatedDate = DateTime.UtcNow;
    }

    public DateTime DateWorked => Date;

    public static Result<Timesheet> Create(string employeeName, int projectId, DateTime date, decimal hoursWorked, string? description = null)
    {
        var validationResult = ValidateTimesheetData(employeeName, projectId, date, hoursWorked, description);
        if (validationResult.IsFailure)
            return Result.Failure<Timesheet>(validationResult.Error!);

        return new Timesheet(employeeName, projectId, date, hoursWorked, description);
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
    public Result UpdateTimeEntry(string employeeName, int projectId, DateTime date, decimal hoursWorked, string? description = null)
    {
        if (!CanBeModified())
            return Result.Failure(TimesheetError.CannotModifyOldEntry);

        var validationResult = ValidateTimesheetData(employeeName, projectId, date, hoursWorked, description);
        if (validationResult.IsFailure)
            return Result.Failure(validationResult.Error!);

        EmployeeName = employeeName;
        ProjectId = projectId;
        Date = date;
        HoursWorked = hoursWorked;
        Description = description;
        UpdatedDate = DateTime.UtcNow;

        return Result.Success();
    }

    public Result UpdateDescription(string? description)
    {
        if (!CanBeModified())
            return Result.Failure(TimesheetError.CannotModifyOldEntry);

        if (description?.Length > 500)
            return Result.Failure(TimesheetError.DescriptionTooLong);

        Description = description;
        UpdatedDate = DateTime.UtcNow;
        return Result.Success();
    }

    public Result AdjustHours(decimal newHours)
    {
        if (!CanBeModified())
            return Result.Failure(TimesheetError.CannotModifyOldEntry);

        if (newHours < 0.1m || newHours > 24)
            return Result.Failure(TimesheetError.InvalidHoursWorked);

        HoursWorked = newHours;
        UpdatedDate = DateTime.UtcNow;
        return Result.Success();
    }

    public bool IsOvertimeEntry => HoursWorked > 8;

    public bool IsWeekendEntry => Date.DayOfWeek == DayOfWeek.Saturday || Date.DayOfWeek == DayOfWeek.Sunday;

    public bool IsRecentEntry => (DateTime.UtcNow - CreatedDate).TotalDays <= 7;

    public bool CanBeModified()
    {
        return (DateTime.UtcNow - Date).TotalDays <= 30;
    }

    private static Result ValidateTimesheetData(string employeeName, int projectId, DateTime date, decimal hoursWorked, string? description)
    {
        if (string.IsNullOrWhiteSpace(employeeName))
            return Result.Failure(TimesheetError.EmployeeNameCannotBeEmpty);

        if (employeeName.Length > 100)
            return Result.Failure(TimesheetError.EmployeeNameTooLong);

        if (description?.Length > 500)
            return Result.Failure(TimesheetError.DescriptionTooLong);

        if (projectId <= 0)
            return Result.Failure(TimesheetError.InvalidProjectId);

        if (hoursWorked < 0.1m || hoursWorked > 24)
            return Result.Failure(TimesheetError.InvalidHoursWorked);

        if (date > DateTime.UtcNow.Date)
            return Result.Failure(TimesheetError.FutureDateNotAllowed);

        return Result.Success();
    }
}

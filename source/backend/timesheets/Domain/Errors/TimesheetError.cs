namespace timesheets.Domain.Errors;

public static class TimesheetError
{
    public static readonly DomainError EmployeeNameCannotBeEmpty = new TimesheetValidationError(
        "Timesheet.EmployeeNameCannotBeEmpty",
        "Employee name cannot be empty");

    public static readonly DomainError EmployeeNameTooLong = new TimesheetValidationError(
        "Timesheet.EmployeeNameTooLong",
        "Employee name cannot exceed 100 characters");

    public static readonly DomainError DescriptionTooLong = new TimesheetValidationError(
        "Timesheet.DescriptionTooLong",
        "Description cannot exceed 500 characters");

    public static readonly DomainError InvalidProjectId = new TimesheetValidationError(
        "Timesheet.InvalidProjectId",
        "Project ID must be a positive number");

    public static readonly DomainError InvalidHoursWorked = new TimesheetValidationError(
        "Timesheet.InvalidHoursWorked",
        "Hours worked must be between 0.1 and 24");

    public static readonly DomainError FutureDateNotAllowed = new TimesheetValidationError(
        "Timesheet.FutureDateNotAllowed",
        "Cannot log time for future dates");

    public static readonly DomainError NotFound = new TimesheetValidationError(
        "Timesheet.NotFound",
        "Timesheet entry not found");

    public static readonly DomainError CannotModifyOldEntry = new TimesheetValidationError(
        "Timesheet.CannotModifyOldEntry",
        "Cannot modify timesheet entries older than 30 days");

    public static readonly DomainError ProjectNotFound = new TimesheetValidationError(
        "Timesheet.ProjectNotFound",
        "The specified project does not exist");

    public static readonly DomainError ProjectInactive = new TimesheetValidationError(
        "Timesheet.ProjectInactive",
        "Cannot log time to an inactive project");

    public static readonly DomainError DuplicateEntry = new TimesheetValidationError(
        "Timesheet.DuplicateEntry",
        "A timesheet entry already exists for this employee, project, and date");

    private sealed record TimesheetValidationError(string Code, string Message) : DomainError(Code, Message);
}
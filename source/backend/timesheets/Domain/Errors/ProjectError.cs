namespace timesheets.Domain.Errors;

public static class ProjectError
{
    public static readonly DomainError NameCannotBeEmpty = new ProjectValidationError(
        "Project.NameCannotBeEmpty",
        "Project name cannot be empty");

    public static readonly DomainError NameTooLong = new ProjectValidationError(
        "Project.NameTooLong",
        "Project name cannot exceed 200 characters");

    public static readonly DomainError DescriptionTooLong = new ProjectValidationError(
        "Project.DescriptionTooLong",
        "Project description cannot exceed 1000 characters");

    public static readonly DomainError ClientNameTooLong = new ProjectValidationError(
        "Project.ClientNameTooLong",
        "Client name cannot exceed 100 characters");

    public static readonly DomainError EndDateMustBeAfterStartDate = new ProjectValidationError(
        "Project.EndDateMustBeAfterStartDate",
        "End date must be after start date");

    public static readonly DomainError NotFound = new ProjectValidationError(
        "Project.NotFound",
        "Project not found");

    public static readonly DomainError CannotDeleteActiveProject = new ProjectValidationError(
        "Project.CannotDeleteActiveProject",
        "Cannot delete an active project with existing timesheet entries");

    public static readonly DomainError ProjectInactive = new ProjectValidationError(
        "Project.ProjectInactive",
        "Cannot log time to an inactive project");

    private sealed record ProjectValidationError(string Code, string Message) : DomainError(Code, Message);
}
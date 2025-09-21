using FluentAssertions;
using timesheets.Domain.Errors;

namespace timesheets.Tests.Unit.Domain.Errors;

public class DomainErrorTests
{
    [Fact]
    public void ProjectError_ShouldHaveCorrectErrorCodes()
    {
        // Assert
        ProjectError.NameCannotBeEmpty.Code.Should().Be("Project.NameCannotBeEmpty");
        ProjectError.NameTooLong.Code.Should().Be("Project.NameTooLong");
        ProjectError.DescriptionTooLong.Code.Should().Be("Project.DescriptionTooLong");
        ProjectError.ClientNameTooLong.Code.Should().Be("Project.ClientNameTooLong");
        ProjectError.EndDateMustBeAfterStartDate.Code.Should().Be("Project.EndDateMustBeAfterStartDate");
        ProjectError.NotFound.Code.Should().Be("Project.NotFound");
        ProjectError.CannotDeleteActiveProject.Code.Should().Be("Project.CannotDeleteActiveProject");
        ProjectError.ProjectInactive.Code.Should().Be("Project.ProjectInactive");
    }

    [Fact]
    public void ProjectError_ShouldHaveCorrectErrorMessages()
    {
        // Assert
        ProjectError.NameCannotBeEmpty.Message.Should().Be("Project name cannot be empty");
        ProjectError.NameTooLong.Message.Should().Be("Project name cannot exceed 200 characters");
        ProjectError.DescriptionTooLong.Message.Should().Be("Project description cannot exceed 1000 characters");
        ProjectError.ClientNameTooLong.Message.Should().Be("Client name cannot exceed 100 characters");
        ProjectError.EndDateMustBeAfterStartDate.Message.Should().Be("End date must be after start date");
        ProjectError.NotFound.Message.Should().Be("Project not found");
        ProjectError.CannotDeleteActiveProject.Message.Should().Be("Cannot delete an active project with existing timesheet entries");
        ProjectError.ProjectInactive.Message.Should().Be("Cannot log time to an inactive project");
    }

    [Fact]
    public void TimesheetError_ShouldHaveCorrectErrorCodes()
    {
        // Assert
        TimesheetError.EmployeeNameCannotBeEmpty.Code.Should().Be("Timesheet.EmployeeNameCannotBeEmpty");
        TimesheetError.EmployeeNameTooLong.Code.Should().Be("Timesheet.EmployeeNameTooLong");
        TimesheetError.DescriptionTooLong.Code.Should().Be("Timesheet.DescriptionTooLong");
        TimesheetError.InvalidProjectId.Code.Should().Be("Timesheet.InvalidProjectId");
        TimesheetError.InvalidHoursWorked.Code.Should().Be("Timesheet.InvalidHoursWorked");
        TimesheetError.FutureDateNotAllowed.Code.Should().Be("Timesheet.FutureDateNotAllowed");
        TimesheetError.NotFound.Code.Should().Be("Timesheet.NotFound");
        TimesheetError.CannotModifyOldEntry.Code.Should().Be("Timesheet.CannotModifyOldEntry");
        TimesheetError.ProjectNotFound.Code.Should().Be("Timesheet.ProjectNotFound");
        TimesheetError.ProjectInactive.Code.Should().Be("Timesheet.ProjectInactive");
        TimesheetError.DuplicateEntry.Code.Should().Be("Timesheet.DuplicateEntry");
    }

    [Fact]
    public void TimesheetError_ShouldHaveCorrectErrorMessages()
    {
        // Assert
        TimesheetError.EmployeeNameCannotBeEmpty.Message.Should().Be("Employee name cannot be empty");
        TimesheetError.EmployeeNameTooLong.Message.Should().Be("Employee name cannot exceed 100 characters");
        TimesheetError.DescriptionTooLong.Message.Should().Be("Description cannot exceed 500 characters");
        TimesheetError.InvalidProjectId.Message.Should().Be("Project ID must be a positive number");
        TimesheetError.InvalidHoursWorked.Message.Should().Be("Hours worked must be between 0.1 and 24");
        TimesheetError.FutureDateNotAllowed.Message.Should().Be("Cannot log time for future dates");
        TimesheetError.NotFound.Message.Should().Be("Timesheet entry not found");
        TimesheetError.CannotModifyOldEntry.Message.Should().Be("Cannot modify timesheet entries older than 30 days");
        TimesheetError.ProjectNotFound.Message.Should().Be("The specified project does not exist");
        TimesheetError.ProjectInactive.Message.Should().Be("Cannot log time to an inactive project");
        TimesheetError.DuplicateEntry.Message.Should().Be("A timesheet entry already exists for this employee, project, and date");
    }

    [Fact]
    public void DomainError_ImplicitConversionToString_ShouldReturnCode()
    {
        // Arrange
        var error = ProjectError.NameCannotBeEmpty;

        // Act
        string code = error;

        // Assert
        code.Should().Be("Project.NameCannotBeEmpty");
    }

    [Fact]
    public void DomainError_ToString_ShouldReturnCodeAndMessage()
    {
        // Arrange
        var error = TimesheetError.InvalidHoursWorked;

        // Act
        var result = error.ToString();

        // Assert - ToString for records includes both properties
        result.Should().Contain("Timesheet.InvalidHoursWorked");
        result.Should().Contain("Hours worked must be between 0.1 and 24");
    }

    [Fact]
    public void DomainError_Equality_ShouldWorkCorrectly()
    {
        // Arrange
        var error1 = ProjectError.NameCannotBeEmpty;
        var error2 = ProjectError.NameCannotBeEmpty;
        var error3 = ProjectError.NameTooLong;

        // Act & Assert
        error1.Should().Be(error2);
        error1.Should().NotBe(error3);
        (error1 == error2).Should().BeTrue();
        (error1 == error3).Should().BeFalse();
    }
}
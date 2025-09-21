using FluentAssertions;
using timesheets.Domain.Entities;
using timesheets.Domain.Errors;
using timesheets.Domain.Shared;

namespace timesheets.Tests.Unit.Domain.Entities;

public class TimesheetTests
{
    [Fact]
    public void Create_WithValidData_ShouldReturnSuccessResult()
    {
        // Arrange
        var employeeName = "John Doe";
        var projectId = 1;
        var date = DateTime.UtcNow.Date;
        var hoursWorked = 8.5m;
        var description = "Working on feature X";

        // Act
        var result = Timesheet.Create(employeeName, projectId, date, hoursWorked, description);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.EmployeeName.Should().Be(employeeName);
        result.Value.ProjectId.Should().Be(projectId);
        result.Value.Date.Should().Be(date);
        result.Value.HoursWorked.Should().Be(hoursWorked);
        result.Value.Description.Should().Be(description);
        result.Value.CreatedDate.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(5));
    }


    [Fact]
    public void Create_WithEmployeeNameTooLong_ShouldReturnFailureResult()
    {
        // Arrange
        var employeeName = new string('a', 101); // Exceeds 100 char limit
        var projectId = 1;
        var date = DateTime.UtcNow.Date;
        var hoursWorked = 8.0m;

        // Act
        var result = Timesheet.Create(employeeName, projectId, date, hoursWorked);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(TimesheetError.EmployeeNameTooLong);
    }

    [Fact]
    public void Create_WithDescriptionTooLong_ShouldReturnFailureResult()
    {
        // Arrange
        var employeeName = "John Doe";
        var projectId = 1;
        var date = DateTime.UtcNow.Date;
        var hoursWorked = 8.0m;
        var description = new string('a', 501); // Exceeds 500 char limit

        // Act
        var result = Timesheet.Create(employeeName, projectId, date, hoursWorked, description);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(TimesheetError.DescriptionTooLong);
    }

    [Fact]
    public void Create_WithInvalidProjectId_ShouldReturnFailureResult()
    {
        // Arrange
        var employeeName = "John Doe";
        var projectId = 0; // Invalid project ID
        var date = DateTime.UtcNow.Date;
        var hoursWorked = 8.0m;

        // Act
        var result = Timesheet.Create(employeeName, projectId, date, hoursWorked);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(TimesheetError.InvalidProjectId);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(0.05)]
    [InlineData(24.1)]
    [InlineData(25)]
    public void Create_WithInvalidHoursWorked_ShouldReturnFailureResult(decimal invalidHours)
    {
        // Arrange
        var employeeName = "John Doe";
        var projectId = 1;
        var date = DateTime.UtcNow.Date;

        // Act
        var result = Timesheet.Create(employeeName, projectId, date, invalidHours);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(TimesheetError.InvalidHoursWorked);
    }
}
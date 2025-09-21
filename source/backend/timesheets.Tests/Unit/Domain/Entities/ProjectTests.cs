using FluentAssertions;
using timesheets.Domain.Entities;
using timesheets.Domain.Errors;
using timesheets.Domain.Shared;

namespace timesheets.Tests.Unit.Domain.Entities;

public class ProjectTests
{
    [Fact]
    public void Create_WithValidData_ShouldReturnSuccessResult()
    {
        // Arrange
        var name = "Test Project";
        var description = "Test Description";
        var startDate = DateTime.UtcNow;
        var endDate = DateTime.UtcNow.AddDays(30);
        var client = "Test Client";

        // Act
        var result = Project.Create(name, description, startDate, endDate, client);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Name.Should().Be(name);
        result.Value.Description.Should().Be(description);
        result.Value.StartDate.Should().Be(startDate);
        result.Value.EndDate.Should().Be(endDate);
        result.Value.Client.Should().Be(client);
        result.Value.IsActive.Should().BeTrue();
        result.Value.CreatedDate.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(5));
    }

    [Fact]
    public void Create_WithNameTooLong_ShouldReturnFailureResult()
    {
        // Arrange
        var name = new string('a', 201); // Exceeds 200 char limit
        var description = "Test Description";

        // Act
        var result = Project.Create(name, description);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(ProjectError.NameTooLong);
    }

    [Fact]
    public void Create_WithDescriptionTooLong_ShouldReturnFailureResult()
    {
        // Arrange
        var name = "Test Project";
        var description = new string('a', 1001); // Exceeds 1000 char limit

        // Act
        var result = Project.Create(name, description);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(ProjectError.DescriptionTooLong);
    }

    [Fact]
    public void Create_WithClientNameTooLong_ShouldReturnFailureResult()
    {
        // Arrange
        var name = "Test Project";
        var description = "Test Description";
        var client = new string('a', 101); // Exceeds 100 char limit

        // Act
        var result = Project.Create(name, description, null, null, client);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(ProjectError.ClientNameTooLong);
    }

}
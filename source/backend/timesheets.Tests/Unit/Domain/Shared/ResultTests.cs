using FluentAssertions;
using timesheets.Domain.Errors;
using timesheets.Domain.Shared;

namespace timesheets.Tests.Unit.Domain.Shared;

public class ResultTests
{
    [Fact]
    public void Success_ShouldCreateSuccessfulResult()
    {
        // Act
        var result = Result.Success();

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.IsFailure.Should().BeFalse();
        result.Error.Should().BeNull();
    }

    [Fact]
    public void Failure_ShouldCreateFailedResult()
    {
        // Arrange
        var error = ProjectError.NameCannotBeEmpty;

        // Act
        var result = Result.Failure(error);

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(error);
    }

    [Fact]
    public void Constructor_WithSuccessAndError_ShouldThrowException()
    {
        // Act & Assert
        var act = () => new TestableResult(true, ProjectError.NameCannotBeEmpty);
        act.Should().Throw<ArgumentException>()
           .WithMessage("Invalid result state*");
    }

    [Fact]
    public void Constructor_WithFailureAndNoError_ShouldThrowException()
    {
        // Act & Assert
        var act = () => new TestableResult(false, null);
        act.Should().Throw<ArgumentException>()
           .WithMessage("Invalid result state*");
    }

    [Fact]
    public void GenericSuccess_ShouldCreateSuccessfulResultWithValue()
    {
        // Arrange
        var value = "test value";

        // Act
        var result = Result.Success(value);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.IsFailure.Should().BeFalse();
        result.Error.Should().BeNull();
        result.Value.Should().Be(value);
    }

    [Fact]
    public void GenericFailure_ShouldCreateFailedResultWithError()
    {
        // Arrange
        var error = TimesheetError.InvalidHoursWorked;

        // Act
        var result = Result.Failure<string>(error);

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(error);
    }

    [Fact]
    public void GenericResult_AccessingValueOnFailure_ShouldThrowException()
    {
        // Arrange
        var result = Result.Failure<string>(ProjectError.NotFound);

        // Act & Assert
        var act = () => result.Value;
        act.Should().Throw<InvalidOperationException>()
           .WithMessage("Cannot access value of failed result");
    }

    [Fact]
    public void ImplicitConversion_FromValue_ShouldCreateSuccessResult()
    {
        // Arrange
        var value = 42;

        // Act
        Result<int> result = value;

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().Be(value);
    }

    [Fact]
    public void ImplicitConversion_FromError_ShouldCreateFailureResult()
    {
        // Arrange
        var error = TimesheetError.NotFound;

        // Act
        Result<string> result = error;

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(error);
    }

    [Fact]
    public void GenericResult_Constructor_WithSuccessAndError_ShouldThrowException()
    {
        // Act & Assert
        var act = () => new TestableGenericResult<string>("value", true, ProjectError.NameCannotBeEmpty);
        act.Should().Throw<ArgumentException>()
           .WithMessage("Invalid result state*");
    }

    [Fact]
    public void GenericResult_Constructor_WithFailureAndNoError_ShouldThrowException()
    {
        // Act & Assert
        var act = () => new TestableGenericResult<string>(null, false, null);
        act.Should().Throw<ArgumentException>()
           .WithMessage("Invalid result state*");
    }

    [Fact]
    public void IsFailure_ShouldBeOppositeOfIsSuccess()
    {
        // Arrange
        var successResult = Result.Success();
        var failureResult = Result.Failure(ProjectError.NotFound);

        // Assert
        successResult.IsFailure.Should().Be(!successResult.IsSuccess);
        failureResult.IsFailure.Should().Be(!failureResult.IsSuccess);
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public void GenericResult_IsFailure_ShouldBeOppositeOfIsSuccess(bool isSuccess)
    {
        // Arrange
        var result = isSuccess
            ? Result.Success("test")
            : Result.Failure<string>(TimesheetError.NotFound);

        // Assert
        result.IsFailure.Should().Be(!result.IsSuccess);
    }

    // Test helper classes to access protected constructors
    private class TestableResult : Result
    {
        public TestableResult(bool isSuccess, DomainError? error) : base(isSuccess, error) { }
    }

    private class TestableGenericResult<T> : Result<T>
    {
        public TestableGenericResult(T? value, bool isSuccess, DomainError? error)
            : base(value, isSuccess, error) { }
    }
}
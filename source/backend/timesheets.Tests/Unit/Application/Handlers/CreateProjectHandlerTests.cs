using AutoMapper;
using FluentAssertions;
using Moq;
using timesheets.Application.Commands.Projects;
using timesheets.Application.DTOs;
using timesheets.Application.Handlers.Projects;
using timesheets.Domain.Entities;
using timesheets.Domain.Errors;
using timesheets.Domain.Interfaces;

namespace timesheets.Tests.Unit.Application.Handlers;

public class CreateProjectHandlerTests
{
    private readonly Mock<IProjectRepository> _mockProjectRepository;
    private readonly Mock<IMapper> _mockMapper;
    private readonly CreateProjectHandler _handler;

    public CreateProjectHandlerTests()
    {
        _mockProjectRepository = new Mock<IProjectRepository>();
        _mockMapper = new Mock<IMapper>();
        _handler = new CreateProjectHandler(_mockProjectRepository.Object, _mockMapper.Object);
    }

    [Fact]
    public async Task Handle_WithValidCommand_ShouldReturnSuccessResult()
    {
        // Arrange
        var command = new CreateProjectCommand(
            Name: "Test Project",
            Description: "Test Description",
            Client: "Test Client",
            StartDate: DateTime.UtcNow,
            EndDate: DateTime.UtcNow.AddDays(30)
        );

        var project = Project.Create(command.Name, command.Description, command.StartDate, command.EndDate, command.Client).Value;
        var expectedDto = new ProjectDto
        {
            Id = 1,
            Name = command.Name,
            Description = command.Description,
            Client = command.Client,
            StartDate = command.StartDate,
            EndDate = command.EndDate,
            IsActive = true,
            CreatedDate = DateTime.UtcNow,
            UpdatedDate = null
        };

        _mockProjectRepository
            .Setup(x => x.AddAsync(It.IsAny<Project>()))
            .ReturnsAsync(project);

        _mockMapper
            .Setup(x => x.Map<ProjectDto>(It.IsAny<Project>()))
            .Returns(expectedDto);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().NotBeNull();
        result.Value.Name.Should().Be(command.Name);
        result.Value.Description.Should().Be(command.Description);
        result.Value.Client.Should().Be(command.Client);

        _mockProjectRepository.Verify(x => x.AddAsync(It.IsAny<Project>()), Times.Once);
        _mockMapper.Verify(x => x.Map<ProjectDto>(It.IsAny<Project>()), Times.Once);
    }

    [Fact]
    public async Task Handle_WithInvalidProjectName_ShouldReturnFailureResult()
    {
        // Arrange
        var command = new CreateProjectCommand(
            Name: "", // Invalid empty name
            Description: "Test Description",
            Client: "Test Client",
            StartDate: DateTime.UtcNow,
            EndDate: DateTime.UtcNow.AddDays(30)
        );

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(ProjectError.NameCannotBeEmpty);

        _mockProjectRepository.Verify(x => x.AddAsync(It.IsAny<Project>()), Times.Never);
        _mockMapper.Verify(x => x.Map<ProjectDto>(It.IsAny<Project>()), Times.Never);
    }

    [Fact]
    public async Task Handle_WithInvalidDateRange_ShouldReturnFailureResult()
    {
        // Arrange
        var command = new CreateProjectCommand(
            Name: "Test Project",
            Description: "Test Description",
            Client: "Test Client",
            StartDate: DateTime.UtcNow,
            EndDate: DateTime.UtcNow.AddDays(-1) // End date before start date
        );

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(ProjectError.EndDateMustBeAfterStartDate);

        _mockProjectRepository.Verify(x => x.AddAsync(It.IsAny<Project>()), Times.Never);
        _mockMapper.Verify(x => x.Map<ProjectDto>(It.IsAny<Project>()), Times.Never);
    }

    [Fact]
    public async Task Handle_WithNameTooLong_ShouldReturnFailureResult()
    {
        // Arrange
        var longName = new string('a', 201); // Exceeds 200 character limit
        var command = new CreateProjectCommand(
            Name: longName,
            Description: "Test Description",
            Client: "Test Client",
            StartDate: DateTime.UtcNow,
            EndDate: DateTime.UtcNow.AddDays(30)
        );

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(ProjectError.NameTooLong);

        _mockProjectRepository.Verify(x => x.AddAsync(It.IsAny<Project>()), Times.Never);
        _mockMapper.Verify(x => x.Map<ProjectDto>(It.IsAny<Project>()), Times.Never);
    }

    [Fact]
    public async Task Handle_WithValidMinimalData_ShouldReturnSuccessResult()
    {
        // Arrange
        var command = new CreateProjectCommand(
            Name: "Minimal Project",
            Description: null,
            Client: null,
            StartDate: null,
            EndDate: null
        );

        var project = Project.Create(command.Name, command.Description, command.StartDate, command.EndDate, command.Client).Value;
        var expectedDto = new ProjectDto
        {
            Id = 1,
            Name = command.Name,
            Description = null,
            Client = null,
            StartDate = null,
            EndDate = null,
            IsActive = true,
            CreatedDate = DateTime.UtcNow,
            UpdatedDate = null
        };

        _mockProjectRepository
            .Setup(x => x.AddAsync(It.IsAny<Project>()))
            .ReturnsAsync(project);

        _mockMapper
            .Setup(x => x.Map<ProjectDto>(It.IsAny<Project>()))
            .Returns(expectedDto);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().NotBeNull();
        result.Value.Name.Should().Be(command.Name);
        result.Value.Description.Should().BeNull();
        result.Value.Client.Should().BeNull();
        result.Value.StartDate.Should().BeNull();
        result.Value.EndDate.Should().BeNull();

        _mockProjectRepository.Verify(x => x.AddAsync(It.IsAny<Project>()), Times.Once);
        _mockMapper.Verify(x => x.Map<ProjectDto>(It.IsAny<Project>()), Times.Once);
    }
}
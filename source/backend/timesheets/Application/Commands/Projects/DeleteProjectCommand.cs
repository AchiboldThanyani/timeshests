using MediatR;

namespace timesheets.Application.Commands.Projects;

public record DeleteProjectCommand(int Id) : IRequest<bool>;
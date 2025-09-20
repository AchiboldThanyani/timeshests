using MediatR;
using timesheets.Domain.Shared;

namespace timesheets.Application.Commands.Projects;

public record DeleteProjectCommand(int Id) : IRequest<Result>;
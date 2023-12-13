using MediatR;

namespace Business.Features.Roles;

public sealed record CreateRoleCommand(
    string Name) : IRequest<Unit>;

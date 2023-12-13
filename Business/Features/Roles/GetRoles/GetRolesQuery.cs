using MediatR;

namespace Business.Features.Roles.GetRoles;

public sealed record GetRolesQuery() : IRequest<List<GetRolesQueryResponse>>;
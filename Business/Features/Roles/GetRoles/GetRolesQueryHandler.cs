using Entities.Models;
using Entities.Repositories;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Business.Features.Roles.GetRoles;

internal sealed class GetRolesQueryHandler : IRequestHandler<GetRolesQuery, List<GetRolesQueryResponse>>
{
    private readonly IRoleRepository _roleRepository;
    private readonly IUnitOfWork _unitOfWork;

    public GetRolesQueryHandler(IRoleRepository roleRepository, IUnitOfWork unitOfWork)
    {
        _roleRepository = roleRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<List<GetRolesQueryResponse>> Handle(GetRolesQuery request, CancellationToken cancellationToken)
    {
        var roleList = await _roleRepository
            .GetAll()
            .Select(x => new GetRolesQueryResponse(x!.Id, x.Name!)).ToListAsync(cancellationToken);
        return roleList;
    }
}
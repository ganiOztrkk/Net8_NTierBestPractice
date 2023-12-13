using Entities.Models;
using Entities.Repositories;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Business.Features.Roles;

internal sealed class CreateRoleCommandHandler : IRequestHandler<CreateRoleCommand, Unit>
{
    private readonly IRoleRepository _roleRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateRoleCommandHandler(IRoleRepository roleRepository, IUnitOfWork unitOfWork)
    {
        _roleRepository = roleRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
    {
        var checkRoleIsExist = await _roleRepository.AnyAsync(x=>x.Name == request.Name, cancellationToken);
        if (checkRoleIsExist)
            throw new ArgumentException("Role is already exist");

        AppRole appRole = new()
        {
            Id = Guid.NewGuid(),
            Name = request.Name
        };
        await _roleRepository.AddAsync(appRole, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
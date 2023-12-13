using Entities.Models;
using Entities.Repositories;
using MediatR;

namespace Business.Features.UserRoles.SetUserRole;

public sealed record SetUserRoleCommand(
    Guid UserId,
    Guid RoleId) : IRequest<Unit>;

internal sealed class UserRoleCommandHandler : IRequestHandler<SetUserRoleCommand, Unit>
{
    private readonly IAppUserRoleRepository _appUserRoleRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UserRoleCommandHandler(IAppUserRoleRepository appUserRoleRepository, IUnitOfWork unitOfWork)
    {
        _appUserRoleRepository = appUserRoleRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(SetUserRoleCommand request, CancellationToken cancellationToken)
    {
        var checkIsRoleSetExist = await _appUserRoleRepository.AnyAsync(x => x.AppUserId == request.UserId && x.AppRoleId == request.RoleId, cancellationToken);
        if (checkIsRoleSetExist)
            throw new ArgumentException("role already set");

        AppUserRole appUserRole = new()
        {
            AppRoleId = request.RoleId,
            AppUserId = request.UserId
        };

        await _appUserRoleRepository.AddAsync(appUserRole, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
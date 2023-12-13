using Entities.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Business.Features.Auth.Register;

internal sealed class RegisterCommandHandler : IRequestHandler<RegisterCommand, Unit>
{
    private readonly UserManager<AppUser> _userManager;

    public RegisterCommandHandler(UserManager<AppUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<Unit> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        var checkUserNameExists = await _userManager.FindByNameAsync(request.UserName);
        if (checkUserNameExists is not null)
            throw new ArgumentException("Username already exist");
        var checkEmailExists = await _userManager.FindByEmailAsync(request.Email);

        if (checkEmailExists is not null)
            throw new ArgumentException("Mail already exist");
        AppUser appUser = new()
        {
            Id = Guid.NewGuid(),
            Email = request.Email,
            Name = request.Name,
            Lastname = request.Lastname,
            UserName = request.UserName
        };

        await _userManager.CreateAsync(appUser, request.Password);

        return Unit.Value;
    }
}
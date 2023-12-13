using AutoMapper;
using Entities.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Business.Features.Auth.Register;

internal sealed class RegisterCommandHandler : IRequestHandler<RegisterCommand, Unit>
{
    private readonly UserManager<AppUser> _userManager;
    private readonly IMapper _mapper;
    public RegisterCommandHandler(UserManager<AppUser> userManager, IMapper mapper)
    {
        _userManager = userManager;
        _mapper = mapper;
    }

    public async Task<Unit> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        var checkUserNameExists = await _userManager.FindByNameAsync(request.UserName);
        if (checkUserNameExists is not null)
            throw new ArgumentException("Username already exist");
        var checkEmailExists = await _userManager.FindByEmailAsync(request.Email);

        if (checkEmailExists is not null)
            throw new ArgumentException("Mail already exist");
        var appUser = _mapper.Map<AppUser>(request);

        await _userManager.CreateAsync(appUser, request.Password);

        return Unit.Value;
    }
}
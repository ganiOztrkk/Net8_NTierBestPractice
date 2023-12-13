using Business.Features.UserRoles.SetUserRole;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebApi.Abstractions;

namespace WebApi.Controllers;

public sealed class AppUserRolesController : ApiController
{
    public AppUserRolesController(IMediator mediator) : base(mediator)
    {
    }


    [HttpPost]
    public async Task<IActionResult> SetRole(SetUserRoleCommand request, CancellationToken cancellationToken)
    {
        await _mediator.Send(request, cancellationToken);

        return NoContent();
    }
}
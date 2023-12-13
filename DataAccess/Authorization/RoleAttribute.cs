using System.Security.Claims;
using Entities.Repositories;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Authorization;

public class RoleAttribute : Attribute, IAuthorizationFilter
{
    private readonly string _role;
    private readonly IAppUserRoleRepository _roleRepository;

    public RoleAttribute(string role, IAppUserRoleRepository roleRepository)
    {
        _role = role;
        _roleRepository = roleRepository;
    }

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var userIdClaim = context.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
        if (userIdClaim is null)
        {
            context.Result = new UnauthorizedResult();
            return;
        }

        var userHasRole = _roleRepository
            .GetWhere(x => x!.AppUserId.ToString() == userIdClaim.Value)
            .Include(x => x!.AppRole)
            .Any(x => x!.AppRole.Name == _role);

        if (!userHasRole)
        {
            context.Result = new UnauthorizedResult();
            return;
        }
    }
}
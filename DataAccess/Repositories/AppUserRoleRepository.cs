using DataAccess.Context;
using Entities.Models;
using Entities.Repositories;

namespace DataAccess.Repositories;

internal sealed class AppUserRoleRepository : Repository<AppUserRole>, IAppUserRoleRepository
{
    public AppUserRoleRepository(ApplicationDbContext context) : base(context)
    {
    }
}

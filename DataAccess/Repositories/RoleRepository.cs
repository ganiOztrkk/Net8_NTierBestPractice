using DataAccess.Context;
using Entities.Models;
using Entities.Repositories;

namespace DataAccess.Repositories;

internal sealed class RoleRepository : Repository<AppRole>, IRoleRepository
{
    public RoleRepository(ApplicationDbContext context) : base(context)
    {
    }
}
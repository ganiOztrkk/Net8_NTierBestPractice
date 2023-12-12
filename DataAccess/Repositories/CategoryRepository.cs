using DataAccess.Context;
using Entities.Models;
using Entities.Repositories;

namespace DataAccess.Repositories;

internal sealed class CategoryRepository : Repository<Category>, ICategoryRepository
{
    public CategoryRepository(ApplicationDbContext context) : base(context)
    {
    }
}

using DataAccess.Context;
using Entities.Models;
using Entities.Repositories;

namespace DataAccess.Repositories;

internal sealed class ProductRepository : Repository<Product>, IProductRepository
{
    public ProductRepository(ApplicationDbContext context) : base(context)
    {
    }
}
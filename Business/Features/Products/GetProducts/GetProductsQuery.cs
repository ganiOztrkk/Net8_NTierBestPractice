using Entities.Models;
using Entities.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Business.Features.Products.GetProducts;

public sealed record GetProductsQuery() : IRequest<List<Product>>;

internal sealed class GetProductsQueryHandler : IRequestHandler<GetProductsQuery, List<Product>>
{
    private readonly IProductRepository _productRepository;

    public GetProductsQueryHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<List<Product>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
    {

        var productList = await _productRepository
            .GetAll()
            .ToListAsync(cancellationToken);
        return productList!;
    }
}
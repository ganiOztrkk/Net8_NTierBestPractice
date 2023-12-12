using MediatR;

namespace Business.Features.Products.UpdateProduct;

public sealed record UpdateProductCommand(
    Guid Id,
    Guid CategoryId,
    string Name,
    decimal Price,
    int Quantity) : IRequest;

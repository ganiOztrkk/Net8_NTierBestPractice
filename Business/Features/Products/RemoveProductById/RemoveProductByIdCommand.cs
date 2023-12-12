using MediatR;

namespace Business.Features.Products.RemoveProductById;

public sealed record RemoveProductByIdCommand(
    Guid Id) : IRequest;
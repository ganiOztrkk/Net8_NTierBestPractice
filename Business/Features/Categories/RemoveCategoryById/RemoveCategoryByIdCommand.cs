using MediatR;

namespace Business.Features.Categories.RemoveCategoryById;

public sealed record RemoveCategoryByIdCommand(
    Guid Id) : IRequest;
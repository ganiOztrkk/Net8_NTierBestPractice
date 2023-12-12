using Entities.Models;
using MediatR;

namespace Business.Features.Categories.UpdateCategory;

public sealed record UpdateCategoryCommand(
    Guid Id,
    string Name) : IRequest;

using MediatR;

namespace Business.Features.Categories.CreateCategory;

public sealed record CreateCategoryCommand(
    string Name) :IRequest ;

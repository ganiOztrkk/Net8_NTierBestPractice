using Entities.Models;
using MediatR;

namespace Business.Features.Categories.GetCategories;

public sealed record GetCategoriesQuery() : IRequest<List<Category>>;
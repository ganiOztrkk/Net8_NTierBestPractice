using Business.Features.Categories.CreateCategory;
using Business.Features.Categories.GetCategories;
using Business.Features.Categories.RemoveCategoryById;
using Business.Features.Categories.UpdateCategory;
using DataAccess.Authorization;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebApi.Abstractions;

namespace WebApi.Controllers;

public sealed class CategoriesController : ApiController
{
    public CategoriesController(IMediator mediator) : base(mediator)
    {
    }

    [HttpPost]
    [RoleFilter("Category.Add")]
    public async Task<IActionResult> Add(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        await _mediator.Send(request, cancellationToken);

        return Created();
    }

    [HttpPost]
    [RoleFilter("Category.Update")]
    public async Task<IActionResult> Update(UpdateCategoryCommand request, CancellationToken cancellationToken)
    {
        await _mediator.Send(request, cancellationToken);

        return NoContent();
    }

    [HttpPost]
    [RoleFilter("Category.Remove")]
    public async Task<IActionResult> RemoveById(RemoveCategoryByIdCommand request, CancellationToken cancellationToken)
    {
        await _mediator.Send(request, cancellationToken);

        return NoContent();
    }

    [HttpPost]
    [RoleFilter("Category.GetAll")]
    public async Task<IActionResult> GetAll(GetCategoriesQuery request, CancellationToken cancellationToken)
    {
        var categoryList = await _mediator.Send(request, cancellationToken);
        return Ok(categoryList);
    }
}
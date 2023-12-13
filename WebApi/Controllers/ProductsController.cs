using Business.Features.Products.CreateProduct;
using Business.Features.Products.GetProducts;
using Business.Features.Products.RemoveProductById;
using Business.Features.Products.UpdateProduct;
using DataAccess.Authorization;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebApi.Abstractions;

namespace WebApi.Controllers;

public class ProductsController : ApiController
{
    public ProductsController(IMediator mediator) : base(mediator)
    {
    }

    [HttpPost]
    [RoleFilter("Product.Add")]
    public async Task<IActionResult> Add(CreateProductCommand request, CancellationToken cancellationToken)
    {
        await _mediator.Send(request, cancellationToken);

        return Created();
    }

    [HttpPost]
    [RoleFilter("Product.Update")]
    public async Task<IActionResult> Update(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        await _mediator.Send(request, cancellationToken);

        return NoContent();
    }

    [HttpPost]
    [RoleFilter("Product.Remove")]
    public async Task<IActionResult> RemoveById(RemoveProductByIdCommand request, CancellationToken cancellationToken)
    {
        await _mediator.Send(request, cancellationToken);

        return NoContent();
    }

    [HttpPost]
    [RoleFilter("Product.GetAll")]
    public async Task<IActionResult> GetAll(GetProductsQuery request, CancellationToken cancellationToken)
    {
        var productList = await _mediator.Send(request, cancellationToken);
        return Ok(productList);
    }
}
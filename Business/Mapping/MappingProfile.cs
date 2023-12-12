using AutoMapper;
using Business.Features.Categories.CreateCategory;
using Business.Features.Categories.GetCategories;
using Business.Features.Categories.UpdateCategory;
using Business.Features.Products.CreateProduct;
using Business.Features.Products.GetProducts;
using Business.Features.Products.UpdateProduct;
using Entities.Models;

namespace Business.Mapping;

internal sealed class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<CreateProductCommand, Product>().ReverseMap();
        CreateMap<UpdateProductCommand, Product>().ReverseMap();
        CreateMap<GetProductsQuery, Product>().ReverseMap();

        CreateMap<CreateCategoryCommand, Category>().ReverseMap();
        CreateMap<UpdateCategoryCommand, Category>().ReverseMap();
        CreateMap<GetCategoriesQuery, Category>().ReverseMap();
    }
}


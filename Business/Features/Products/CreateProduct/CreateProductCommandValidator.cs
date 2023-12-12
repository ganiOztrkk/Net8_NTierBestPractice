using FluentValidation;

namespace Business.Features.Products.CreateProduct;

public sealed class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .NotNull()
            .WithMessage("name cannot be null or empty");
        RuleFor(x => x.Name)
            .MinimumLength(2)
            .WithMessage("minimum 2 characters");
        RuleFor(x => x.Name)
            .MaximumLength(15)
            .WithMessage("maximum 15 characters");

        RuleFor(x => x.CategoryId)
            .NotNull()
            .NotEmpty()
            .WithMessage("category cannot be null or empty");
    }
}
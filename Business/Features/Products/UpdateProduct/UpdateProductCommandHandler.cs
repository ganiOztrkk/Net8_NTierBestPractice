using AutoMapper;
using Entities.Repositories;
using MediatR;

namespace Business.Features.Products.UpdateProduct;

public sealed class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand>
{
    private readonly IProductRepository _productRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UpdateProductCommandHandler(IProductRepository productRepository, IUnitOfWork unitOfWork, IMapper mapper)
    {
        _productRepository = productRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetByIdAsync(x => x.Id == request.Id, cancellationToken);
        if (product is null)
            throw new ArgumentException("not found");
        var isProductExist = await _productRepository.AnyAsync(x => x.Name == request.Name, cancellationToken);
        if (isProductExist)
            throw new ArgumentException("already exist");

        _mapper.Map(request, product);

        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
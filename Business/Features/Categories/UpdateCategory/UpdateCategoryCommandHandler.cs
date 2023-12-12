using AutoMapper;
using Entities.Repositories;
using MediatR;

namespace Business.Features.Categories.UpdateCategory;

internal sealed class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand>
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UpdateCategoryCommandHandler(ICategoryRepository categoryRepository, IUnitOfWork unitOfWork, IMapper mapper)
    {
        _categoryRepository = categoryRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = await _categoryRepository.GetByIdAsync(x => x.Id == request.Id, cancellationToken);
        if (category is null)
            throw new ArgumentException("not found");

        if (request.Name != category.Name)
            throw new ArgumentException("already exist");

        _mapper.Map(request, category);

        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
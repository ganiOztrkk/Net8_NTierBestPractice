using System;
using FluentValidation;
using FluentValidation.Results;
using MediatR;

namespace Business.Behaviors;

public sealed class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where
    TRequest : class,
    IRequest<TResponse>
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (!_validators.Any()) 
            return await next();
        //assembly de herhangi bir validasyon var mı check - eger yoksa devam et.

        var context = new ValidationContext<TRequest>(request);
        //eger validasyon varsa context oluşturdum. validasyon kuralları validationcontext üzerinden denetleniyor.


        var errorDictionary = _validators
            .Select(x => x.Validate(context))
            .SelectMany(x => x.Errors)
            .Where(x => x is not null)
            .GroupBy(
            x => x.PropertyName,
            x => x.ErrorMessage, (propertyName, errorMessage) => new
            {
                Key = propertyName,
                Values = errorMessage.Distinct().ToArray()
            })
            .ToDictionary(x => x.Key, x => x.Values[0]);
        //var olan validasyon kurallarıma göre oluşabilecek bir hata varsa bunları dictionary içerisine set ettik.


        if (errorDictionary.Any())
        {
            var errors = errorDictionary.Select(x => new ValidationFailure
            {
                PropertyName = x.Value,
                ErrorCode = x.Key
            });
            throw new ValidationException(errors);
        }
        //dictionaryde hata var mı check. eğer varsa bunları validasyon hatası olarak ekrana ver.


        return await next();
        //eğer yoksa ve içi boşsa devam et.


        //artık herhangi bir commandı çağırırken eğer validatorumuz varsa otomatik olarak requestten sonra ilk önce validator tetiklenecek eğer sıkıntı yoksa handler metodumuza devam edecek.
    }
}
using System.Threading;
using System.Threading.Tasks;

using Domain.Validation;

using FluentValidation.Results;

using MediatR;

namespace Application.Commands
{
    public abstract class
        BaseRequestHandler<TRequest, TResponse> : IRequestHandler<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        protected readonly IEventPublisher EventPublisher;

        protected BaseRequestHandler(IEventPublisher eventPublisher)
        {
            EventPublisher = eventPublisher;
        }

        public abstract Task<TResponse> Handle(TRequest request,
            CancellationToken cancellationToken);

        protected ValidationResult NotEmpty<T>(T item)
        {
            return new NotEmptyValidator<T>().Validate(item);
        }

        protected static ValidationResult NotNull<T>(T item)
        {
            return new NotNullValidator<T>().Validate(item);
        }

        protected static ValidationResult Exists<T>(T item)
            where T : class
        {
            ValidationResult res = new ValidationResult();
            if (item == null)
            {
                ValidationFailure failure =
                    new ValidationFailure("NotFound", "Item was not found.");
                res.Errors.Add(failure);
            }

            return res;
        }
    }
}
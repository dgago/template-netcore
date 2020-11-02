using System.Threading;
using System.Threading.Tasks;

using MediatR;

namespace Kit.Application.Handlers
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

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken)
        {
            TResponse response = await HandleRequest(request, cancellationToken);

            await EventPublisher.PublishAsync(new DomainEvent<TRequest>(request),
                cancellationToken);

            return response;
        }

        protected abstract Task<TResponse> HandleRequest(TRequest request,
            CancellationToken cancellationToken);
    }
}
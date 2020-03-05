using System.Threading;
using System.Threading.Tasks;

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
            this.EventPublisher = eventPublisher;
        }

        public abstract Task<TResponse> Handle(TRequest request,
            CancellationToken cancellationToken);
    }
}
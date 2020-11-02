using System.Threading;
using System.Threading.Tasks;

using MediatR;

namespace Kit.Application.Handlers
{
    public class MediatorEventPublisher : IEventPublisher
    {
        private readonly IMediator _mediator;

        public MediatorEventPublisher(IMediator mediator)
        {
            _mediator = mediator;
        }

        public Task PublishAsync<TNotification>(TNotification notification,
            CancellationToken cancellationToken = default)
            where TNotification : INotification
        {
            return _mediator.Publish(notification, cancellationToken);
        }

        public Task<TResponse> SendAsync<TResponse>(IRequest<TResponse> request,
            CancellationToken cancellationToken = default)
        {
            return _mediator.Send(request, cancellationToken);
        }
    }
}
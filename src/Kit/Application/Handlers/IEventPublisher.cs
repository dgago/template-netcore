using System.Threading;
using System.Threading.Tasks;

using MediatR;

namespace Kit.Application.Handlers
{
    public interface IEventPublisher
    {
        Task PublishAsync<TNotification>(TNotification notification,
            CancellationToken cancellationToken = default)
            where TNotification : INotification;

        Task<TResponse> SendAsync<TResponse>(IRequest<TResponse> request,
            CancellationToken cancellationToken = default);
    }
}
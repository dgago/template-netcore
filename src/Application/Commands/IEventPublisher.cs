using System.Threading;
using System.Threading.Tasks;

using MediatR;

namespace Application.Commands
{
    public interface IEventPublisher
    {
        Task Publish<TNotification>(TNotification notification,
            CancellationToken cancellationToken = default)
            where TNotification : INotification;

        Task<TResponse> Send<TResponse>(IRequest<TResponse> request,
            CancellationToken cancellationToken = default);
    }
}
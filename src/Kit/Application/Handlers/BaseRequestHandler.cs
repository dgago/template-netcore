using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

using MediatR;

using Microsoft.Extensions.Logging;

namespace Kit.Application.Handlers
{
    public abstract class
        BaseRequestHandler<TRequest, TResponse> : IRequestHandler<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        protected readonly IEventPublisher EventPublisher;
        protected readonly ILogger<TRequest> Logger;

        protected BaseRequestHandler(IEventPublisher eventPublisher, ILogger<TRequest> logger)
        {
            EventPublisher = eventPublisher;
            Logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();

            string requestName = $"{request.GetType().Name} [{Guid.NewGuid().ToString()}]";
            Logger.LogInformation($"[START] {requestName}");

            TResponse response;
            try
            {
                response = await HandleRequest(request, cancellationToken);

                await EventPublisher.PublishAsync(new DomainEvent<TRequest>(request),
                    cancellationToken);
            }
            finally
            {
                stopwatch.Stop();
                Logger.LogInformation(
                    $"[END] {requestName}; ElapsedMilliseconds={stopwatch.ElapsedMilliseconds}ms");
            }


            return response;
        }

        protected abstract Task<TResponse> HandleRequest(TRequest request,
            CancellationToken cancellationToken);
    }
}
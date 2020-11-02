using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

using MediatR;

using Microsoft.Extensions.Logging;

namespace Kit.Application.Logging
{
    public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly ILogger<TRequest> _logger;

        public LoggingBehavior(ILogger<TRequest> logger)
        {
            _logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request,
            CancellationToken cancellationToken,
            RequestHandlerDelegate<TResponse> next)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();

            string requestName = $"{request.GetType().Name} [{Guid.NewGuid().ToString()}]";
            _logger.LogInformation($"[START] {requestName}");

            TResponse response;

            try
            {
                // try
                // {
                //     _logger.LogInformation(
                //         $"[PROPS] {requestNameWithGuid} {JsonSerializer.Serialize(request)}");
                // }
                // catch (NotSupportedException)
                // {
                //     _logger.LogInformation(
                //         $"[Serialization ERROR] {requestNameWithGuid} Could not serialize the request.");
                // }

                response = await next();
            }
            finally
            {
                stopwatch.Stop();
                _logger.LogInformation(
                    $"[END] {requestName}; ElapsedMilliseconds={stopwatch.ElapsedMilliseconds}ms");
            }

            return response;
        }
    }
}
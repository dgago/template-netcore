﻿using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using Kit.Application.Handlers;

using MediatR;

namespace Test.Mocks
{
    public class MockEventPublisher : IEventPublisher
    {
        private readonly IMediator _mediator;

        public readonly IList<INotification> Notifications;

        public MockEventPublisher(IMediator mediator)
        {
            _mediator = mediator;
            Notifications = new List<INotification>();
        }

        public Task PublishAsync<TNotification>(TNotification notification,
            CancellationToken cancellationToken = default)
            where TNotification : INotification
        {
            Notifications.Add(notification);
            return _mediator.Publish(notification, cancellationToken);
        }

        public Task<TResponse> SendAsync<TResponse>(IRequest<TResponse> request,
            CancellationToken cancellationToken = default)
        {
            return _mediator.Send(request, cancellationToken);
        }
    }
}
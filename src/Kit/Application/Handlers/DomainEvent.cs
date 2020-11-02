using System;

using MediatR;

namespace Kit.Application.Handlers
{
    public class DomainEvent<T> : INotification
    {
        public DomainEvent(T data)
        {
            CreatedAt = DateTime.UtcNow;
            Data = data;
        }

        public T Data { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
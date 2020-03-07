using System;

using MediatR;

namespace Application.Commands
{
    public abstract class DomainEvent : INotification
    {
        protected DomainEvent()
        {
            CreatedAt = DateTime.UtcNow;
        }

        public DomainEvent(DateTime createdAt)
        {
            CreatedAt = createdAt;
        }

        public DateTime CreatedAt { get; set; }
    }
}
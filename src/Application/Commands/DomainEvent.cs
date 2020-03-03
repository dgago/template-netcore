using System;

using MediatR;

namespace Application.Commands
{
    public abstract class DomainEvent : INotification
    {
        protected DomainEvent()
        {
            this.CreatedAt = DateTime.UtcNow;
        }

        public DomainEvent(DateTime createdAt)
        {
            this.CreatedAt = createdAt;
        }

        public DateTime CreatedAt { get; set; }
    }
}
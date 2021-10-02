using System;
using System.Collections.Generic;
using MediatR;

namespace QuizTopics.Common.DomainDriven
{
    public abstract class Entity
    {
        private readonly List<INotification> domainEvents = new();

        protected Entity()
        {
        }

        protected Entity(Guid id)
            : this()
        {
            this.Id = id;
        }

        public IEnumerable<INotification> DomainEvents => this.domainEvents;

        public Guid Id { get; protected set; }

        public bool SoftDeleted { get; protected set; }

        protected void AddDomainEvent(INotification eventItem)
        {
            this.domainEvents.Add(eventItem);
        }

        public void ClearDomainEvents()
        {
            this.domainEvents.Clear();
        }

        public override bool Equals(object? obj)
        {
            if (obj is not Entity other)
            {
                return false;
            }

            if (ReferenceEquals(this, other))
            {
                return true;
            }

            if (this.GetType() != other.GetType())
            {
                return false;
            }

            if (this.Id == Guid.Empty || other.Id == Guid.Empty)
            {
                return false;
            }

            return this.Id == other.Id;
        }

        public static bool operator ==(Entity? a, Entity? b)
        {
            if (a is null && b is null)
            {
                return true;
            }

            if (a is null || b is null)
            {
                return false;
            }

            return a.Equals(b);
        }

        public static bool operator !=(Entity? a, Entity? b)
        {
            return !(a == b);
        }

        public override int GetHashCode()
        {
            return (this.GetType().ToString() + this.Id).GetHashCode();
        }
    }
}
using System;

namespace Domain
{
    public abstract class Entity
    {
        protected Entity()
        {
            Id = Guid.NewGuid();
            CreatedAt = DateTime.UtcNow;
        }

        public Guid Id { get; private set; }
        public DateTime CreatedAt { get; private set; }
    }
}

using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace Shortener.Domain
{
    public abstract class Entity
    {
        protected Entity()
        {
            //Id = Guid.NewGuid();
            CreatedAt = DateTime.UtcNow;
        }

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        //public ObjectId Id { get; set; }
        //public ObjectId _id { get; set; }
        //public Guid Id { get; private set; }
        public DateTime CreatedAt { get; private set; }
    }
}

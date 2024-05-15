using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace GlobalThings.Domain.Entities.Base
{
    public class EntityBase
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        public required bool Active { get; set; }

    }
}

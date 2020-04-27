using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace FooGooBusiness.MongoDb
{
    /// <summary>
    /// This is the main "thing" that this microservice controls.
    /// </summary>
    public class FooDoc
    {
        [BsonId]
        public ObjectId Id { get; set; }

        [BsonElement("foo_id")]
        public Guid FooId { get; set; }

        [BsonElement("foo_type_id")]
        public Guid FooTypeId { get; set; }

        [BsonElement("name")]
        public string Name { get; set; }

        [BsonElement("active")]
        public bool Active { get; set; }
    }
}
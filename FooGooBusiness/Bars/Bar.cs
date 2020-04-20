using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace FooGooBusiness.Bars
{
    public class Bar
    {
        [BsonId]
        public ObjectId Id { get; set; }

        [BsonElement("bar_id")]
        public Guid BarId { get; set; }

        [BsonElement("foo_id")]
        public Guid FooId { get; set; }

        [BsonElement("name")]
        public string Name { get; set; }

        [BsonElement("active")]
        public bool Active { get; set; }
    }
}
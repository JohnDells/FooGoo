using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace FooGooMongoDb
{
    /// <summary>
    /// This class is intended to be a "category" or "group" of Foos, and
    ///     given the limited number of entries can be cached to provide a lookup / match.
    /// </summary>
    public class FooTypeDoc
    {
        [BsonId]
        public ObjectId Id { get; set; }

        [BsonElement("foo_type_id")]
        public Guid FooTypeId { get; set; }

        [BsonElement("name")]
        public string Name { get; set; }

        [BsonElement("active")]
        public bool Active { get; set; }
    }
}
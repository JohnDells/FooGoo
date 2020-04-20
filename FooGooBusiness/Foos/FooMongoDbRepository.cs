using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FooGooBusiness.Foos
{
    public class FooMongoDbRepository : IFooRespository
    {
        private readonly string _databaseName = "FooGoo";
        private readonly string _collectionName = "Foos";
        private readonly string _connectionString;
        private readonly IMongoClient _client;
        private readonly IMongoDatabase _database;
        private readonly IMongoCollection<Foo> _collection;

        public FooMongoDbRepository(string connectionString)
        {
            _connectionString = connectionString;
            _client = new MongoClient(_connectionString);
            _database = _client.GetDatabase(_databaseName);
            _collection = _database.GetCollection<Foo>(_collectionName);
        }

        public async Task<List<Foo>> GetAllActiveFoos()
        {
            return await _collection.Find<Foo>(Builders<Foo>.Filter.Eq((x) => x.Active, true)).ToListAsync();
        }

        public async Task<List<Foo>> GetActiveFoosByType(Guid fooTypeId)
        {
            return await _collection.Find<Foo>(Builders<Foo>.Filter.Eq((x) => x.Active, true) & Builders<Foo>.Filter.Eq((x) => x.FooTypeId, fooTypeId)).ToListAsync();
        }

        public async Task<Foo> GetFoo(Guid id)
        {
            var filter = Builders<Foo>.Filter;
            return await _collection.Find<Foo>(filter.Eq((x) => x.FooId, id) & filter.Eq((x) => x.Active, true)).FirstOrDefaultAsync();
        }

        public async Task InsertFoo(string name)
        {
            var foo = new Foo() { FooId = Guid.NewGuid(), Name = name, Active = true };

            using (var session = await _client.StartSessionAsync())
            {
                await _collection.InsertOneAsync(session, foo);
            }
        }

        public async Task UpdateFooName(Guid id, string name)
        {
            using (var session = await _client.StartSessionAsync())
            {
                await _collection.UpdateOneAsync(session, Builders<Foo>.Filter.Eq((x) => x.FooId, id), Builders<Foo>.Update.Set((x) => x.Name, name));
            }
        }

        public async Task UpdateFooTypeId(Guid id, Guid fooTypeId)
        {
            using (var session = await _client.StartSessionAsync())
            {
                await _collection.UpdateOneAsync(session, Builders<Foo>.Filter.Eq((x) => x.FooId, id), Builders<Foo>.Update.Set((x) => x.FooTypeId, fooTypeId));
            }
        }

        public async Task DeactivateFoo(Guid id)
        {
            using (var session = await _client.StartSessionAsync())
            {
                await _collection.UpdateOneAsync(session, Builders<Foo>.Filter.Eq((x) => x.FooId, id), Builders<Foo>.Update.Set((x) => x.Active, false));
            }
        }
    }
}
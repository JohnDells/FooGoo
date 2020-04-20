using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FooGooBusiness.Bars
{
    public class BarMongoDbRepository : IBarRepository
    {
        private readonly string _databaseName = "FooGoo";
        private readonly string _collectionName = "Bars";
        private readonly string _connectionString;
        private readonly IMongoClient _client;
        private readonly IMongoDatabase _database;
        private readonly IMongoCollection<Bar> _collection;

        public BarMongoDbRepository(string connectionString)
        {
            _connectionString = connectionString;
            _client = new MongoClient(_connectionString);
            _database = _client.GetDatabase(_databaseName);
            _collection = _database.GetCollection<Bar>(_collectionName);
        }

        public async Task<List<Bar>> GetAllActiveBarsByFooId(Guid fooId)
        {
            return await _collection.Find<Bar>(Builders<Bar>.Filter.Eq((x) => x.FooId, fooId) & Builders<Bar>.Filter.Eq((x) => x.Active, true)).ToListAsync();
        }

        public async Task InsertBar(Guid fooId, string name)
        {
            var item = new Bar() { BarId = Guid.NewGuid(), FooId = fooId, Name = name, Active = true };

            using (var session = await _client.StartSessionAsync())
            {
                await _collection.InsertOneAsync(session, item);
            }
        }

        public async Task UpdateBarName(Guid id, string name)
        {
            using (var session = await _client.StartSessionAsync())
            {
                await _collection.UpdateOneAsync(session, Builders<Bar>.Filter.Eq((x) => x.BarId, id), Builders<Bar>.Update.Set((x) => x.Name, name));
            }
        }

        public async Task DeactivateBar(Guid id)
        {
            using (var session = await _client.StartSessionAsync())
            {
                await _collection.UpdateOneAsync(session, Builders<Bar>.Filter.Eq((x) => x.BarId, id), Builders<Bar>.Update.Set((x) => x.Active, false));
            }
        }
    }
}
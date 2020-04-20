using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FooGooBusiness.FooTypes
{
    public class FooTypeMongoDbRepository : IFooTypeRepository
    {
        private readonly string _databaseName = "FooGoo";
        private readonly string _collectionName = "FooTypes";
        private readonly string _connectionString;
        private readonly IMongoClient _client;
        private readonly IMongoDatabase _database;
        private readonly IMongoCollection<FooType> _collection;

        public FooTypeMongoDbRepository(string connectionString)
        {
            _connectionString = connectionString;
            _client = new MongoClient(_connectionString);
            _database = _client.GetDatabase(_databaseName);
            _collection = _database.GetCollection<FooType>(_collectionName);
        }

        public async Task<List<FooType>> GetAllActiveFooTypes()
        {
            return await _collection.Find<FooType>(Builders<FooType>.Filter.Eq((x) => x.Active, true)).ToListAsync();
        }

        public async Task InsertFooType(string name)
        {
            var item = new FooType() { FooTypeId = Guid.NewGuid(), Name = name, Active = true };

            using (var session = await _client.StartSessionAsync())
            {
                await _collection.InsertOneAsync(session, item);
            }
        }

        public async Task UpdateFooTypeName(Guid id, string name)
        {
            using (var session = await _client.StartSessionAsync())
            {
                await _collection.UpdateOneAsync(session, Builders<FooType>.Filter.Eq((x) => x.FooTypeId, id), Builders<FooType>.Update.Set((x) => x.Name, name));
            }
        }
    }
}
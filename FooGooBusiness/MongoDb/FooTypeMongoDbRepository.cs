using AutoMapper;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FooGooBusiness.MongoDb
{
    public class FooTypeMongoDbRepository : IFooTypeRepository
    {
        private readonly string _databaseName = "FooGoo";
        private readonly string _collectionName = "FooTypes";
        private readonly string _connectionString;
        private readonly IMongoClient _client;
        private readonly IMongoDatabase _database;
        private readonly IMongoCollection<FooTypeDoc> _collection;
        private readonly IMapper _mapper;

        public FooTypeMongoDbRepository(string connectionString, IMapper mapper)
        {
            _connectionString = connectionString;
            _client = new MongoClient(_connectionString);
            _database = _client.GetDatabase(_databaseName);
            _collection = _database.GetCollection<FooTypeDoc>(_collectionName);
            _mapper = mapper;
        }

        public async Task<List<FooTypeDto>> GetAllActiveFooTypes()
        {
            var items = await _collection.Find<FooTypeDoc>(Builders<FooTypeDoc>.Filter.Eq((x) => x.Active, true)).ToListAsync();
            var result = _mapper.Map<List<FooTypeDto>>(items);
            return result;
        }

        public async Task InsertFooType(string name)
        {
            var item = new FooTypeDoc() { FooTypeId = Guid.NewGuid(), Name = name, Active = true };

            using (var session = await _client.StartSessionAsync())
            {
                await _collection.InsertOneAsync(session, item);
            }
        }

        public async Task UpdateFooTypeName(Guid id, string name)
        {
            using (var session = await _client.StartSessionAsync())
            {
                await _collection.UpdateOneAsync(session, Builders<FooTypeDoc>.Filter.Eq((x) => x.FooTypeId, id), Builders<FooTypeDoc>.Update.Set((x) => x.Name, name));
            }
        }
    }
}
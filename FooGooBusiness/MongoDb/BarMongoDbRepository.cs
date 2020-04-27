using AutoMapper;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FooGooBusiness.MongoDb
{
    public class BarMongoDbRepository : IBarRepository
    {
        private readonly string _databaseName = "FooGoo";
        private readonly string _collectionName = "Bars";
        private readonly string _connectionString;
        private readonly IMongoClient _client;
        private readonly IMongoDatabase _database;
        private readonly IMongoCollection<BarDoc> _collection;
        private readonly IMapper _mapper;

        public BarMongoDbRepository(string connectionString, IMapper mapper)
        {
            _connectionString = connectionString;
            _client = new MongoClient(_connectionString);
            _database = _client.GetDatabase(_databaseName);
            _collection = _database.GetCollection<BarDoc>(_collectionName);
            _mapper = mapper;
        }

        public async Task<List<BarDto>> GetAllActiveBarsByFooId(Guid fooId)
        {
            var items = await _collection.Find<BarDoc>(Builders<BarDoc>.Filter.Eq((x) => x.FooId, fooId) & Builders<BarDoc>.Filter.Eq((x) => x.Active, true)).ToListAsync();
            var result = _mapper.Map<List<BarDto>>(items);
            return result;
        }

        public async Task InsertBar(Guid fooId, string name)
        {
            var item = new BarDoc() { BarId = Guid.NewGuid(), FooId = fooId, Name = name, Active = true };

            using (var session = await _client.StartSessionAsync())
            {
                await _collection.InsertOneAsync(session, item);
            }
        }

        public async Task UpdateBarName(Guid id, string name)
        {
            using (var session = await _client.StartSessionAsync())
            {
                await _collection.UpdateOneAsync(session, Builders<BarDoc>.Filter.Eq((x) => x.BarId, id), Builders<BarDoc>.Update.Set((x) => x.Name, name));
            }
        }

        public async Task DeactivateBar(Guid id)
        {
            using (var session = await _client.StartSessionAsync())
            {
                await _collection.UpdateOneAsync(session, Builders<BarDoc>.Filter.Eq((x) => x.BarId, id), Builders<BarDoc>.Update.Set((x) => x.Active, false));
            }
        }
    }
}
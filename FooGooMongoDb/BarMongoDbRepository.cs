using AutoMapper;
using FooGooBusiness;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FooGooMongoDb
{
    public class BarMongoDbRepository : IBarRepository
    {
        private readonly IMongoClient _client;
        private readonly IMapper _mapper;

        private readonly string _databaseName = MongoDbConstants.DatabaseName;
        private readonly string _collectionName = MongoDbConstants.BarCollection;
        private readonly IMongoDatabase _database;
        private readonly IMongoCollection<BarDoc> _collection;

        public BarMongoDbRepository(IMongoClient client, IMapper mapper)
        {
            _client = client;
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

        public async Task InsertBar(BarDto item)
        {
            var doc = _mapper.Map<BarDoc>(item);
            using (var session = await _client.StartSessionAsync())
            {
                await _collection.InsertOneAsync(session, doc);
            }
        }

        public async Task UpdateBarName(Guid id, string name)
        {
            using (var session = await _client.StartSessionAsync())
            {
                await _collection.UpdateOneAsync(session, Builders<BarDoc>.Filter.Eq((x) => x.BarId, id), Builders<BarDoc>.Update.Set((x) => x.Name, name));
            }
        }

        public async Task RemoveBar(Guid id)
        {
            using (var session = await _client.StartSessionAsync())
            {
                await _collection.UpdateOneAsync(session, Builders<BarDoc>.Filter.Eq((x) => x.BarId, id), Builders<BarDoc>.Update.Set((x) => x.Active, false));
            }
        }
    }
}
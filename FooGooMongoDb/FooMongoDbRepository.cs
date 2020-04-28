using AutoMapper;
using FooGooBusiness;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FooGooMongoDb
{
    public class FooMongoDbRepository : IFooRespository
    {
        private readonly IMongoClient _client;
        private readonly IMapper _mapper;

        private readonly string _databaseName = MongoDbConstants.DatabaseName;
        private readonly string _collectionName = MongoDbConstants.FooCollection;
        private readonly IMongoDatabase _database;
        private readonly IMongoCollection<FooDoc> _collection;

        public FooMongoDbRepository(IMongoClient client, IMapper mapper)
        {
            _client = client;
            _database = _client.GetDatabase(_databaseName);
            _collection = _database.GetCollection<FooDoc>(_collectionName);
            _mapper = mapper;
        }

        public async Task<List<FooDto>> GetAllActiveFoos()
        {
            var items = await _collection.Find<FooDoc>(Builders<FooDoc>.Filter.Eq((x) => x.Active, true)).ToListAsync();
            var result = _mapper.Map<List<FooDto>>(items);
            return result;
        }

        public async Task<List<FooDto>> GetActiveFoosByType(Guid fooTypeId)
        {
            var items = await _collection.Find<FooDoc>(Builders<FooDoc>.Filter.Eq((x) => x.Active, true) & Builders<FooDoc>.Filter.Eq((x) => x.FooTypeId, fooTypeId)).ToListAsync();
            var result = _mapper.Map<List<FooDto>>(items);
            return result;
        }

        public async Task<FooDto> GetFoo(Guid id)
        {
            var filter = Builders<FooDoc>.Filter;
            var items = await _collection.Find<FooDoc>(filter.Eq((x) => x.FooId, id) & filter.Eq((x) => x.Active, true)).FirstOrDefaultAsync();
            var result = _mapper.Map<FooDto>(items);
            return result;
        }

        public async Task CreateFoo(FooDto item)
        {
            var doc = _mapper.Map<FooDoc>(item);
            using (var session = await _client.StartSessionAsync())
            {
                await _collection.InsertOneAsync(session, doc);
            }
        }

        public async Task UpdateFooName(Guid id, string name)
        {
            using (var session = await _client.StartSessionAsync())
            {
                await _collection.UpdateOneAsync(session, Builders<FooDoc>.Filter.Eq((x) => x.FooId, id), Builders<FooDoc>.Update.Set((x) => x.Name, name));
            }
        }

        public async Task UpdateFooTypeId(Guid id, Guid fooTypeId)
        {
            using (var session = await _client.StartSessionAsync())
            {
                await _collection.UpdateOneAsync(session, Builders<FooDoc>.Filter.Eq((x) => x.FooId, id), Builders<FooDoc>.Update.Set((x) => x.FooTypeId, fooTypeId));
            }
        }

        public async Task DeleteFoo(Guid id)
        {
            using (var session = await _client.StartSessionAsync())
            {
                await _collection.UpdateOneAsync(session, Builders<FooDoc>.Filter.Eq((x) => x.FooId, id), Builders<FooDoc>.Update.Set((x) => x.Active, false));
            }
        }
    }
}
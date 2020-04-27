using AutoMapper;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FooGooBusiness.MongoDb
{
    public class FooMongoDbRepository : IFooRespository
    {
        private readonly string _databaseName = "FooGoo";
        private readonly string _collectionName = "Foos";
        private readonly string _connectionString;
        private readonly IMongoClient _client;
        private readonly IMongoDatabase _database;
        private readonly IMongoCollection<FooDoc> _collection;
        private readonly IMapper _mapper;

        public FooMongoDbRepository(string connectionString, IMapper mapper)
        {
            _connectionString = connectionString;
            _client = new MongoClient(_connectionString);
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

        public async Task InsertFoo(Guid fooTypeId, string name)
        {
            var foo = new FooDoc() { FooId = Guid.NewGuid(), FooTypeId = fooTypeId, Name = name, Active = true };

            using (var session = await _client.StartSessionAsync())
            {
                await _collection.InsertOneAsync(session, foo);
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

        public async Task DeactivateFoo(Guid id)
        {
            using (var session = await _client.StartSessionAsync())
            {
                await _collection.UpdateOneAsync(session, Builders<FooDoc>.Filter.Eq((x) => x.FooId, id), Builders<FooDoc>.Update.Set((x) => x.Active, false));
            }
        }
    }
}
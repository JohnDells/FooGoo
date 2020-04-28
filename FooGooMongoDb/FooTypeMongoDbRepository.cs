using AutoMapper;
using FooGooBusiness;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FooGooMongoDb
{
    public class FooTypeMongoDbRepository : IFooTypeRepository
    {
        private readonly IMongoClient _client;
        private readonly IMapper _mapper;

        private readonly string _databaseName = MongoDbConstants.DatabaseName;
        private readonly string _collectionName = MongoDbConstants.FooTypeCollection;
        private readonly IMongoDatabase _database;
        private readonly IMongoCollection<FooTypeDoc> _collection;

        public FooTypeMongoDbRepository(IMongoClient client, IMapper mapper)
        {
            _client = client;
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

        public async Task InsertFooType(FooTypeDto item)
        {
            var doc = _mapper.Map<FooTypeDoc>(item);
            using (var session = await _client.StartSessionAsync())
            {
                await _collection.InsertOneAsync(session, doc);
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
using AutoMapper;
using FooGooBusiness;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FooGooDapper
{
    public class BarDapperRepository : IBarRepository
    {
        private readonly string _connectionString;
        private readonly IMapper _mapper;

        public BarDapperRepository(string connectionString, IMapper mapper)
        {
            _connectionString = connectionString;
            _mapper = mapper;
        }

        public Task<List<BarDto>> GetAllActiveBarsByFooId(Guid fooId)
        {
            throw new NotImplementedException();
        }

        public Task InsertBar(BarDto item)
        {
            throw new NotImplementedException();
        }

        public Task UpdateBarName(Guid id, string name)
        {
            throw new NotImplementedException();
        }

        public Task RemoveBar(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FooGooBusiness.Dapper
{
    public class BarDapperRepository : IBarRepository
    {
        private readonly string _connectionString;

        public BarDapperRepository(string connectionString)
        {
            _connectionString = connectionString;
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
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FooGooBusiness.Dapper
{
    public class FooTypeDapperRespository : IFooTypeRepository
    {
        private readonly string _connectionString;

        public FooTypeDapperRespository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public Task<List<FooTypeDto>> GetAllActiveFooTypes()
        {
            throw new NotImplementedException();
        }

        public Task InsertFooType(FooTypeDto item)
        {
            throw new NotImplementedException();
        }

        public Task UpdateFooTypeName(Guid id, string name)
        {
            throw new NotImplementedException();
        }
    }
}
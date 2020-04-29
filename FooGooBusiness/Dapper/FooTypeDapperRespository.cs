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

        public async Task<List<FooTypeDto>> GetAllActiveFooTypes()
        {
            throw new NotImplementedException();
        }

        public async Task<FooTypeDto> GetFooType(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task CreateFooType(FooTypeDto item)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateFooTypeName(Guid id, string name)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteFooType(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
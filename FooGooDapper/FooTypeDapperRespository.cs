using AutoMapper;
using Dapper;
using FooGooBusiness;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FooGooDapper
{
    public class FooTypeDapperRespository : IFooTypeRepository
    {
        private readonly string _connectionString;
        private readonly IMapper _mapper;

        public FooTypeDapperRespository(string connectionString, IMapper mapper)
        {
            _connectionString = connectionString;
            _mapper = mapper;
        }

        public async Task<List<FooTypeDto>> GetAllActiveFooTypes()
        {
            var query = "SELECT * FROM [dbo].[FooTypes];";

            using (var connection = new SqlConnection(_connectionString))
            {
                var items = await connection.QueryAsync<FooTypeRec>(query);
                var result = _mapper.Map<List<FooTypeDto>>(items);
                return result;
            }
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
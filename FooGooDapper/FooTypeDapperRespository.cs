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

        public async Task<FooTypeDto> GetFooType(Guid id)
        {
            var query = "SELECT * FROM [dbo].[FooTypes] WHERE [FooTypeId] = @FooTypeId;";
            using (var connection = new SqlConnection(_connectionString))
            {
                var items = await connection.QueryAsync<FooTypeRec>(query, new { FooTypeId = id });
                var result = _mapper.Map<FooTypeDto>(items);
                return result;
            }
        }

        public async Task CreateFooType(FooTypeDto item)
        {
            var query = "INSERT INTO [dbo].[FooTypes] ([FooTypeId], [Name], [Active]) VALUES (@FooTypeId, @Name, @Active);";
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.ExecuteAsync(query, new { FooTypeId = item.FooTypeId, Name = item.Name, Active = item.Active });
            }
        }

        public async Task UpdateFooTypeName(Guid id, string name)
        {
            var query = "UPDATE [dbo].[FooTypes] SET [Name] = @Name WHERE [FooTypeId] = @FooTypeId;";
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.ExecuteAsync(query, new { FooTypeId = id, Name = name });
            }
        }

        public async Task DeleteFooType(Guid id)
        {
            var query = "UPDATE [dbo].[FooTypes] SET [Active] = 0 WHERE [FooTypeId] = @BarId;";
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.ExecuteAsync(query, new { FooTypeId = id });
            }
        }
    }
}
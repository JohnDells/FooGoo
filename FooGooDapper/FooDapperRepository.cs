using AutoMapper;
using Dapper;
using FooGooBusiness;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FooGooDapper
{
    public class FooDapperRepository : IFooRespository
    {
        private readonly string _connectionString;
        private readonly IMapper _mapper;

        public FooDapperRepository(string connectionString, IMapper mapper)
        {
            _connectionString = connectionString;
            _mapper = mapper;
        }

        public async Task<List<FooDto>> GetActiveFoosByType(Guid fooTypeId)
        {
            var query = "SELECT * FROM [dbo].[Foos] WHERE [Active] = 1 AND [FooTypeId] = @FooTypeId;";
            using (var connection = new SqlConnection(_connectionString))
            {
                var items = await connection.QueryAsync<FooRec>(query, new { FooTypeId = fooTypeId});
                var result = _mapper.Map<List<FooDto>>(items);
                return result;
            }
        }

        public async Task<List<FooDto>> GetAllActiveFoos()
        {
            var query = "SELECT * FROM [dbo].[Foos] WHERE [Active] = 1;";
            using (var connection = new SqlConnection(_connectionString))
            {
                var items = await connection.QueryAsync<FooRec>(query);
                var result = _mapper.Map<List<FooDto>>(items);
                return result;
            }
        }

        public async Task<FooDto> GetFoo(Guid id)
        {
            var query = "SELECT * FROM [dbo].[Foos] WHERE [Active] = 1 AND [FooId] = @FooId;";
            using (var connection = new SqlConnection(_connectionString))
            {
                var item = await connection.QuerySingleOrDefaultAsync<FooRec>(query, new { FooId = id });
                var result = _mapper.Map<FooDto>(item);
                return result;
            }
        }

        public async Task CreateFoo(FooDto item)
        {
            var query = "INSERT INTO [dbo].[Foos] ([FooId], [FooTypeId], [Name], [Active]) VALUES (@FooId, @FooTypeId, @Name, @Active);";
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.ExecuteAsync(query, new { FooId = item.FooId, FooTypeId = item.FooTypeId, Name = item.Name, Active = item.Active });
            }
        }

        public async Task UpdateFooName(Guid id, string name)
        {
            var query = "UPDATE [dbo].[Foos] SET [Name] = @Name WHERE [FooId] = @FooId;";
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.ExecuteAsync(query, new { FooId = id, Name = name });
            }
        }

        public async Task UpdateFooTypeId(Guid id, Guid fooTypeId)
        {
            var query = "UPDATE [dbo].[Foos] SET [FooTypeId] = @FooTypeId WHERE [FooId] = @FooId;";
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.ExecuteAsync(query, new { FooId = id, FooTypeId = fooTypeId });
            }
        }

        public async Task DeleteFoo(Guid id)
        {
            var query = "UPDATE [dbo].[Foos] SET [Active] = 0 WHERE [FooId] = @FooId;";
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.ExecuteAsync(query, new { FooId = id });
            }
        }
    }
}
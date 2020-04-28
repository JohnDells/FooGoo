using AutoMapper;
using Dapper;
using FooGooBusiness;
using Microsoft.Data.SqlClient;
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

        public async Task<List<BarDto>> GetAllActiveBarsByFooId(Guid fooId)
        {
            var query = "SELECT * FROM [dbo].[Bars] WHERE [Active] = 1 AND [FooId] = @FooId;";

            using (var connection = new SqlConnection(_connectionString))
            {
                var items = await connection.QueryAsync<BarRec>(query, new { FooId = fooId });
                var result = _mapper.Map<List<BarDto>>(items);
                return result;
            }
        }

        public async Task InsertBar(BarDto item)
        {
            var query = "INSERT INTO [dbo].[Bars] ([BarId], [FooId], [Name], [Active]) VALUES (@BarId, @FooId, @Name, @Active);";
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.ExecuteAsync(query, new { BarId = item.BarId, FooId = item.FooId, Name = item.Name, Active = item.Active });
            }
        }

        public async Task UpdateBarName(Guid id, string name)
        {
            var query = "UPDATE [dbo].[Bars] SET [Name] = @Name WHERE [BarId] = @BarId;";
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.ExecuteAsync(query, new { BarId = id, Name = name });
            }
        }

        public async Task RemoveBar(Guid id)
        {
            var query = "UPDATE [dbo].[Bars] SET [Active] = 0 WHERE [BarId] = @BarId;";
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.ExecuteAsync(query, new { BarId = id });
            }
        }
    }
}
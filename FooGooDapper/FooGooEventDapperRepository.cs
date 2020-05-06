using AutoMapper;
using Dapper;
using FooGooBusiness;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FooGooDapper
{
    public class FooGooEventDapperRepository : IFooGooEventRepository
    {
        private readonly string _connectionString;
        private readonly IMapper _mapper;

        public FooGooEventDapperRepository(string connectionString, IMapper mapper)
        {
            _connectionString = connectionString;
            _mapper = mapper;
        }

        public async Task AddFooGooEvents(List<FooGooEventDto> items)
        {
            var query = "INSERT INTO [dbo].[FooGooEvent] ([Type], [Value], [CorrelationId], [CreatedDate], [CreatedBy]) VALUES (@Type, @Value, @CorrelationId, @CreatedDate, @CreatedBy);";
            using (var connection = new SqlConnection(_connectionString))
            {
                foreach (var item in items)
                {
                    var parameters = new { Type = item.Type, Value = item.Value, CorrelationId = item.CorrelationId, CreatedDate = item.CreatedDate, CreatedBy = item.CreatedBy };
                    await connection.ExecuteAsync(query, parameters);
                }
            }
        }

        public async Task<List<FooGooEventDto>> Get(long? minSequenceId = null, long? maxSequenceId = null)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                IEnumerable<FooGooEventRec> items;

                if (minSequenceId != null && maxSequenceId != null)
                {
                    var query = "SELECT * FROM [dbo].[FooGooEvent] WHERE [SequenceId] > @MinSequenceId AND [SequenceId] <= @MaxSequenceId ORDER BY [SequenceId]";
                    items = await connection.QueryAsync<FooGooEventRec>(query, new { MinSequenceId = minSequenceId, MaxSequenceId = maxSequenceId });
                }
                else if (minSequenceId != null)
                {
                    var query = "SELECT * FROM [dbo].[FooGooEvent] WHERE [SequenceId] > @MinSequenceId ORDER BY [SequenceId]";
                    items = await connection.QueryAsync<FooGooEventRec>(query, new { MinSequenceId = minSequenceId });
                }
                else if (maxSequenceId != null)
                {
                    var query = "SELECT * FROM [dbo].[FooGooEvent] WHERE [SequenceId] <= @MaxSequenceId ORDER BY [SequenceId]";
                    items = await connection.QueryAsync<FooGooEventRec>(query, new { MaxSequenceId = maxSequenceId });
                }
                else
                {
                    var query = "SELECT * FROM [dbo].[FooGooEvent] ORDER BY [SequenceId]";
                    items = await connection.QueryAsync<FooGooEventRec>(query);
                }

                var result = _mapper.Map<List<FooGooEventDto>>(items.ToList());
                return result;
            }
        }

        public async Task<long?> GetLiveSequenceId()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var query = "SELECT TOP 1 [CurrentSequenceId] FROM [dbo].[FooGooSnapshot] WHERE [SnapshotType] = 0";
                var result = await connection.QueryFirstOrDefaultAsync<long?>(query);
                return result;
            }
        }

        public async Task SetLiveSequenceId(long currentSequenceId, Guid userId)
        {
            var now = DateTime.Now;

            var query = @"IF EXISTS (SELECT 1 FROM [dbo].[FooGooSnapshot] WHERE [SnapshotType] = 0)
                BEGIN
                    UPDATE [dbo].[FooGooSnapshot] SET [CurrentSequenceId] = @CurrentSequenceId, [ModifiedDate] = @ModifiedDate, [ModifiedBy] = @ModifiedBy WHERE [SnapshotType] = 0
                END 
                ELSE
                BEGIN
                    INSERT INTO [dbo].[FooGooSnapshot] ([Id], [CurrentSequenceId], [SnapshotType], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy])
                    VALUES (@Id, @CurrentSequenceId, @SnapshotType, @CreatedDate, @CreatedBy, @ModifiedDate, @ModifiedBy)
                END;";

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.ExecuteAsync(query, new { Id = Guid.NewGuid(), CurrentSequenceId = currentSequenceId, SnapshotType = 0, CreatedDate = now, CreatedBy = userId, ModifiedDate = now, ModifiedBy = userId });
            }
        }
    }
}
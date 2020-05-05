using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FooGooBusiness
{
    public interface IFooGooEventRepository
    {
        Task<long?> GetLiveSequenceId();

        Task SetLiveSequenceId(long currentSequenceId, Guid userId);

        Task<List<FooGooEventDto>> Get(long? minSequenceId = null, long? maxSequenceId = null);

        Task AddFooGooEvents(List<FooGooEventDto> items);
    }
}
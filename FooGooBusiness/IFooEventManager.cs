using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FooGooBusiness
{
    public interface IFooEventManager
    {
        Task Process(Guid userId);

        Task ProcessFooEventAsync(IFooGooEvent item);

        Task AddEvents(FooGooEventsDto items, Guid userId);

        Task AddEvents(List<IFooGooEvent> events, Guid userId, Guid? correlationId = null);
    }
}
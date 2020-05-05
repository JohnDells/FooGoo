using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FooGooBusiness
{
    public interface IFooEventManager
    {
        Task ProcessFooEventAsync(IFooGooEvent item);

        Task AddEvents(List<IFooGooEvent> events, Guid createdBy, Guid? correlationId = null);
    }
}
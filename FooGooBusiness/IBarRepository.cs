using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FooGooBusiness
{
    public interface IBarRepository
    {
        Task<List<BarDto>> GetAllActiveBarsByFooId(Guid fooId);

        Task CreateBar(BarDto item);

        Task UpdateBarName(Guid id, string name);

        Task DeleteBar(Guid id);
    }
}
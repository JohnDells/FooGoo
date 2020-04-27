using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FooGooBusiness
{
    public interface IBarRepository
    {
        Task<List<BarDto>> GetAllActiveBarsByFooId(Guid fooId);

        Task InsertBar(Guid fooId, string name);

        Task UpdateBarName(Guid id, string name);

        Task DeactivateBar(Guid id);
    }
}
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FooGooBusiness.Bars
{
    public interface IBarRepository
    {
        Task<List<Bar>> GetAllActiveBarsByFooId(Guid fooId);

        Task InsertBar(Guid fooId, string name);

        Task UpdateBarName(Guid id, string name);
    }
}
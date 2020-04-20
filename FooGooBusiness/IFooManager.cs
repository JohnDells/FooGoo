using FooGooBusiness.Bars;
using FooGooBusiness.Foos;
using FooGooBusiness.FooTypes;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FooGooBusiness
{
    public interface IFooManager
    {
        Task<List<Foo>> GetAllActiveFoos();

        Task<List<Foo>> GetAllActiveFoosByType(Guid fooTypeId);

        Task<Foo> GetFoo(Guid id);

        Task InsertFoo(string name);

        Task UpdateFooName(Guid id, string name);

        Task UpdateFooTypeId(Guid id, Guid fooTypeId);

        Task DeactivateFoo(Guid id);

        Task<List<FooType>> GetAllActiveFooTypes();

        Task InsertFooType(string name);

        Task UpdateFooTypeName(Guid id, string name);

        Task<List<Bar>> GetAllActiveBarsByFooId(Guid fooId);

        Task InsertBar(Guid fooId, string name);

        Task UpdateBarName(Guid id, string name);
    }
}
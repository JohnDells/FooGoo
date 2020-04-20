using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FooGooBusiness.Foos
{
    public interface IFooRespository
    {
        Task<List<Foo>> GetAllActiveFoos();

        Task<List<Foo>> GetActiveFoosByType(Guid fooTypeId);

        Task<Foo> GetFoo(Guid id);

        Task InsertFoo(string name);

        Task UpdateFooName(Guid id, string name);

        Task UpdateFooTypeId(Guid id, Guid fooTypeId);

        Task DeactivateFoo(Guid id);
    }
}
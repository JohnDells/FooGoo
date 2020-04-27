using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FooGooBusiness
{
    public interface IFooManager
    {
        Task<List<FooDto>> GetAllActiveFoos();

        Task<List<FooDto>> GetAllActiveFoosByType(Guid fooTypeId);

        Task<FooDto> GetFoo(Guid id);

        Task InsertFoo(FooDto item);

        Task UpdateFooName(Guid id, string name);

        Task UpdateFooTypeId(Guid id, Guid fooTypeId);

        Task DeactivateFoo(Guid id);

        Task<List<FooTypeDto>> GetAllActiveFooTypes();

        Task InsertFooType(FooTypeDto item);

        Task UpdateFooTypeName(Guid id, string name);

        Task<List<BarDto>> GetAllActiveBarsByFooId(Guid fooId);

        Task InsertBar(BarDto item);

        Task UpdateBarName(Guid id, string name);

        Task DeactivateBar(Guid id);
    }
}
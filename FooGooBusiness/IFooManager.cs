using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FooGooBusiness
{
    public interface IFooManager
    {
        Task<List<FooTypeDto>> GetAllActiveFooTypes();

        Task<FooTypeDto> GetFooType(Guid id);

        Task CreateFooType(FooTypeDto item);

        Task UpdateFooTypeName(Guid id, string name);

        Task DeleteFooType(Guid id);

        Task<List<FooDto>> GetAllActiveFoos();

        Task<List<FooDto>> GetAllActiveFoosByType(Guid fooTypeId);

        Task<FooDto> GetFoo(Guid id);

        Task CreateFoo(FooDto item);

        Task UpdateFooName(Guid id, string name);

        Task UpdateFooTypeId(Guid id, Guid fooTypeId);

        Task DeleteFoo(Guid id);

        Task<List<BarDto>> GetAllActiveBarsByFooId(Guid fooId);

        Task CreateBar(BarDto item);

        Task UpdateBarName(Guid id, string name);

        Task DeleteBar(Guid id);
    }
}
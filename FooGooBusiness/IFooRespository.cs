using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FooGooBusiness
{
    public interface IFooRespository
    {
        Task<List<FooDto>> GetAllActiveFoos();

        Task<List<FooDto>> GetActiveFoosByType(Guid fooTypeId);

        Task<FooDto> GetFoo(Guid id);

        Task InsertFoo(FooDto item);

        Task UpdateFooName(Guid id, string name);

        Task UpdateFooTypeId(Guid id, Guid fooTypeId);

        Task RemoveFoo(Guid id);
    }
}
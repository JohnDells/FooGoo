using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FooGooBusiness
{
    public interface IFooTypeRepository
    {
        Task<List<FooTypeDto>> GetAllActiveFooTypes();

        Task InsertFooType(FooTypeDto item);

        Task UpdateFooTypeName(Guid id, string name);
    }
}
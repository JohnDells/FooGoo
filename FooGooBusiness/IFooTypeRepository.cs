using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FooGooBusiness
{
    public interface IFooTypeRepository
    {
        Task<List<FooTypeDto>> GetAllActiveFooTypes();

        Task CreateFooType(FooTypeDto item);

        Task UpdateFooTypeName(Guid id, string name);

        Task DeleteFooType(Guid id);
    }
}
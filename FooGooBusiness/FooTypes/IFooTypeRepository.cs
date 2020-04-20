using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FooGooBusiness.FooTypes
{
    public interface IFooTypeRepository
    {
        Task<List<FooType>> GetAllActiveFooTypes();

        Task InsertFooType(string name);

        Task UpdateFooTypeName(Guid id, string name);
    }
}
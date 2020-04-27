﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FooGooBusiness
{
    public interface IFooRespository
    {
        Task<List<FooDto>> GetAllActiveFoos();

        Task<List<FooDto>> GetActiveFoosByType(Guid fooTypeId);

        Task<FooDto> GetFoo(Guid id);

        Task InsertFoo(Guid fooTypeId, string name);

        Task UpdateFooName(Guid id, string name);

        Task UpdateFooTypeId(Guid id, Guid fooTypeId);

        Task DeactivateFoo(Guid id);
    }
}
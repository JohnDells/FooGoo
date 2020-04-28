﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FooGooBusiness.Dapper
{
    public class FooDapperRepository : IFooRespository
    {
        private readonly string _connectionString;

        public FooDapperRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public Task<List<FooDto>> GetActiveFoosByType(Guid fooTypeId)
        {
            throw new NotImplementedException();
        }

        public Task<List<FooDto>> GetAllActiveFoos()
        {
            throw new NotImplementedException();
        }

        public Task<FooDto> GetFoo(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task InsertFoo(FooDto item)
        {
            throw new NotImplementedException();
        }

        public Task UpdateFooName(Guid id, string name)
        {
            throw new NotImplementedException();
        }

        public Task UpdateFooTypeId(Guid id, Guid fooTypeId)
        {
            throw new NotImplementedException();
        }

        public Task RemoveFoo(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
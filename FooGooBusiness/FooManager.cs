using FooGooBusiness.Bars;
using FooGooBusiness.Foos;
using FooGooBusiness.FooTypes;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FooGooBusiness
{
    public class FooManager : IFooManager
    {
        private readonly IFooRespository _fooRepository;
        private readonly IFooTypeRepository _fooTypeRepository;
        private readonly IBarRepository _barRepository;

        public FooManager(IFooRespository repository, IFooTypeRepository fooTypeRepository, IBarRepository barRepository)
        {
            _fooRepository = repository;
            _fooTypeRepository = fooTypeRepository;
            _barRepository = barRepository;
        }

        public async Task<List<Foo>> GetAllActiveFoos()
        {
            return await _fooRepository.GetAllActiveFoos();
        }

        public async Task<List<Foo>> GetAllActiveFoosByType(Guid fooTypeId)
        {
            return await _fooRepository.GetActiveFoosByType(fooTypeId);
        }

        public async Task<Foo> GetFoo(Guid id)
        {
            return await _fooRepository.GetFoo(id);
        }

        public async Task InsertFoo(string name)
        {
            await _fooRepository.InsertFoo(name);
        }

        public async Task UpdateFooName(Guid id, string name)
        {
            await _fooRepository.UpdateFooName(id, name);
        }

        public async Task UpdateFooTypeId(Guid id, Guid fooTypeId)
        {
            await _fooRepository.UpdateFooTypeId(id, fooTypeId);
        }

        public async Task DeactivateFoo(Guid id)
        {
            await _fooRepository.DeactivateFoo(id);
        }

        public async Task<List<FooType>> GetAllActiveFooTypes()
        {
            return await _fooTypeRepository.GetAllActiveFooTypes();
        }

        public async Task InsertFooType(string name)
        {
            await _fooTypeRepository.InsertFooType(name);
        }

        public async Task UpdateFooTypeName(Guid id, string name)
        {
            await _fooTypeRepository.UpdateFooTypeName(id, name);
        }

        public async Task<List<Bar>> GetAllActiveBarsByFooId(Guid fooId)
        {
            return await _barRepository.GetAllActiveBarsByFooId(fooId);
        }

        public async Task InsertBar(Guid fooId, string name)
        {
            await _barRepository.InsertBar(fooId, name);
        }

        public async Task UpdateBarName(Guid id, string name)
        {
            await _barRepository.UpdateBarName(id, name);
        }

    }
}
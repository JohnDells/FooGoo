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

        public async Task<List<FooDto>> GetAllActiveFoos()
        {
            return await _fooRepository.GetAllActiveFoos();
        }

        public async Task<List<FooDto>> GetAllActiveFoosByType(Guid fooTypeId)
        {
            return await _fooRepository.GetActiveFoosByType(fooTypeId);
        }

        public async Task<FooDto> GetFoo(Guid id)
        {
            return await _fooRepository.GetFoo(id);
        }

        public async Task CreateFoo(FooDto item)
        {
            await _fooRepository.CreateFoo(item);
        }

        public async Task UpdateFooName(Guid id, string name)
        {
            await _fooRepository.UpdateFooName(id, name);
        }

        public async Task UpdateFooTypeId(Guid id, Guid fooTypeId)
        {
            await _fooRepository.UpdateFooTypeId(id, fooTypeId);
        }

        public async Task DeleteFoo(Guid id)
        {
            await _fooRepository.DeleteFoo(id);
        }

        public async Task<List<FooTypeDto>> GetAllActiveFooTypes()
        {
            return await _fooTypeRepository.GetAllActiveFooTypes();
        }

        public async Task<FooTypeDto> GetFooType(Guid id)
        {
            return await _fooTypeRepository.GetFooType(id);
        }

        public async Task CreateFooType(FooTypeDto item)
        {
            await _fooTypeRepository.CreateFooType(item);
        }

        public async Task UpdateFooTypeName(Guid id, string name)
        {
            await _fooTypeRepository.UpdateFooTypeName(id, name);
        }

        public async Task DeleteFooType(Guid id)
        {
            await _fooTypeRepository.DeleteFooType(id);
        }

        public async Task<List<BarDto>> GetAllActiveBarsByFooId(Guid fooId)
        {
            return await _barRepository.GetAllActiveBarsByFooId(fooId);
        }

        public async Task CreateBar(BarDto item)
        {
            await _barRepository.CreateBar(item);
        }

        public async Task UpdateBarName(Guid id, string name)
        {
            await _barRepository.UpdateBarName(id, name);
        }

        public async Task DeleteBar(Guid id)
        {
            await _barRepository.DeleteBar(id);
        }
    }
}
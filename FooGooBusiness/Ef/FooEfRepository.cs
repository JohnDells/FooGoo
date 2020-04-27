using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FooGooBusiness.Ef
{
    public class FooEfRepository : IFooRespository
    {
        private readonly string _connectionString;
        private readonly IMapper _mapper;
        private readonly FooGooContext _context;

        public FooEfRepository(string connectionString, IMapper mapper)
        {
            _connectionString = connectionString;
            _mapper = mapper;
            _context = new FooGooContext(_connectionString);
        }

        public async Task<List<FooDto>> GetActiveFoosByType(Guid fooTypeId)
        {
            var items = await _context.Foos.Where(x => x.Active && x.FooTypeId == fooTypeId).ToListAsync();
            var result = _mapper.Map<List<FooDto>>(items);
            return result;
        }

        public async Task<List<FooDto>> GetAllActiveFoos()
        {
            var items = await _context.Foos.Where(x => x.Active).ToListAsync();
            var result = _mapper.Map<List<FooDto>>(items);
            return result;
        }

        public async Task<FooDto> GetFoo(Guid id)
        {
            var item = await _context.Foos.Where(x => x.FooId == id).FirstOrDefaultAsync();
            var result = _mapper.Map<FooDto>(item);
            return result;
        }

        public async Task InsertFoo(Guid fooTypeId, string name)
        {
            var item = new FooEntity { FooId = Guid.NewGuid(), FooTypeId = fooTypeId, Name = name, Active = true };

            _context.Foos.Add(item);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateFooName(Guid id, string name)
        {
            var item = await _context.Foos.Where(x => x.FooId == id).FirstOrDefaultAsync();
            if (item != null)
            {
                item.Name = name;
                await _context.SaveChangesAsync();
            }
        }

        public async Task UpdateFooTypeId(Guid id, Guid fooTypeId)
        {
            var item = await _context.Foos.Where(x => x.FooId == id).FirstOrDefaultAsync();
            if (item != null)
            {
                item.FooTypeId = fooTypeId;
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeactivateFoo(Guid id)
        {
            var item = await _context.Foos.Where(x => x.FooId == id).FirstOrDefaultAsync();
            if (item != null)
            {
                item.Active = false;
                await _context.SaveChangesAsync();
            }
        }
    }
}
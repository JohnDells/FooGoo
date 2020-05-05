using AutoMapper;
using FooGooBusiness;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FooGooEf
{
    public class FooEfRepository : IFooRespository
    {
        private readonly IFooGooDbContext _context;
        private readonly IMapper _mapper;

        public FooEfRepository(IFooGooDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
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
            var item = await _context.Foos.Where(x => x.FooId == id && x.Active).FirstOrDefaultAsync();
            var result = _mapper.Map<FooDto>(item);
            return result;
        }

        public async Task CreateFoo(FooDto item)
        {
            var entity = _mapper.Map<FooEntity>(item);
            _context.Foos.Add(entity);
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

        public async Task DeleteFoo(Guid id)
        {
            var item = await _context.Foos.Where(x => x.FooId == id && x.Active).FirstOrDefaultAsync();
            if (item != null)
            {
                item.Active = false;
                await _context.SaveChangesAsync();
            }
        }
    }
}
using AutoMapper;
using FooGooBusiness;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FooGooEf
{
    public class BarEfRepository : IBarRepository
    {
        private readonly IFooGooDbContext _context;
        private readonly IMapper _mapper;

        public BarEfRepository(IFooGooDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<BarDto>> GetAllActiveBarsByFooId(Guid fooId)
        {
            var items = await _context.Bars.Where(x => x.Active).ToListAsync();
            var result = _mapper.Map<List<BarDto>>(items);
            return result;
        }

        public async Task CreateBar(BarDto item)
        {
            var entity = _mapper.Map<BarEntity>(item);
            _context.Bars.Add(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateBarName(Guid id, string name)
        {
            var item = await _context.Bars.Where(x => x.BarId == id).FirstOrDefaultAsync();
            if (item != null)
            {
                item.Name = name;
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteBar(Guid id)
        {
            var item = await _context.Bars.Where(x => x.BarId == id).FirstOrDefaultAsync();
            if (item != null)
            {
                item.Active = false;
                await _context.SaveChangesAsync();
            }
        }
    }
}
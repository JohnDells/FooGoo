using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FooGooBusiness.Ef
{
    public class BarEfRepository : IBarRepository
    {
        private readonly string _connectionString;
        private readonly IMapper _mapper;
        private readonly FooGooContext _context;

        public BarEfRepository(string connectionString, IMapper mapper)
        {
            _connectionString = connectionString;
            _mapper = mapper;
            _context = new FooGooContext(_connectionString);
        }

        public async Task<List<BarDto>> GetAllActiveBarsByFooId(Guid fooId)
        {
            var items = await _context.Bars.Where(x => x.Active).ToListAsync();
            var result = _mapper.Map<List<BarDto>>(items);
            return result;
        }

        public async Task InsertBar(Guid fooId, string name)
        {
            var item = new BarEntity { BarId = Guid.NewGuid(), FooId = fooId, Name = name, Active = true };

            _context.Bars.Add(item);
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

        public async Task DeactivateBar(Guid id)
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
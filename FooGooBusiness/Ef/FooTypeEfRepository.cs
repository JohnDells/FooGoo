using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FooGooBusiness.Ef
{
    public class FooTypeEfRepository : DbContext, IFooTypeRepository
    {
        private readonly IFooGooDbContext _context;
        private readonly IMapper _mapper;

        public FooTypeEfRepository(IFooGooDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<FooTypeDto>> GetAllActiveFooTypes()
        {
            var items = await _context.FooTypes.Where(x => x.Active).ToListAsync();
            var result = _mapper.Map<List<FooTypeDto>>(items);
            return result;
        }

        public async Task InsertFooType(FooTypeDto item)
        {
            var entity = _mapper.Map<FooTypeEntity>(item);
            _context.FooTypes.Add(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateFooTypeName(Guid id, string name)
        {
            var item = await _context.FooTypes.Where(x => x.FooTypeId == id).FirstOrDefaultAsync();
            if (item != null)
            {
                item.Name = name;
                await _context.SaveChangesAsync();
            }
        }
    }
}
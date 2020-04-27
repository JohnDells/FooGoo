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
        private readonly string _connectionString;
        private readonly IMapper _mapper;
        private readonly FooGooContext _context;

        public FooTypeEfRepository(string connectionString, IMapper mapper)
        {
            _connectionString = connectionString;
            _mapper = mapper;
            _context = new FooGooContext(_connectionString);
        }

        public async Task<List<FooTypeDto>> GetAllActiveFooTypes()
        {
            var items = await _context.FooTypes.Where(x => x.Active).ToListAsync();
            var result = _mapper.Map<List<FooTypeDto>>(items);
            return result;
        }

        public async Task InsertFooType(string name)
        {
            var item = new FooTypeEntity { FooTypeId = Guid.NewGuid(), Name = name, Active = true };

            _context.FooTypes.Add(item);
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
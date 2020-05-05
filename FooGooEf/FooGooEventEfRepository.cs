using AutoMapper;
using FooGooBusiness;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FooGooEf
{
    public class FooGooEventEfRepository : IFooGooEventRepository
    {
        private readonly IFooGooDbContext _context;
        private readonly IMapper _mapper;

        public FooGooEventEfRepository(IFooGooDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<long?> GetLiveSequenceId()
        {
            return await _context.FooGooSnapshots.Where(x => x.SnapshotType == 0).Select(x => (long?)x.CurrentSequenceId).FirstOrDefaultAsync();
        }

        public async Task SetLiveSequenceId(long currentSequenceId, Guid userId)
        {
            var now = DateTime.Now;
            var item = await _context.FooGooSnapshots.Where(x => x.SnapshotType == 0).FirstOrDefaultAsync();
            if (item == null)
            {
                var id = Guid.NewGuid();
                item = new FooGooSnapshotEntity { Id = id, SnapshotType = 0, CreatedDate = now, CreatedBy = userId };
                _context.FooGooSnapshots.Add(item);
            }

            item.CurrentSequenceId = currentSequenceId;
            item.ModifiedDate = now;
            item.ModifiedBy = userId;

            await _context.SaveChangesAsync();
        }

        public async Task<List<FooGooEventDto>> Get(long? minSequenceId = null, long? maxSequenceId = null)
        {
            IQueryable<FooGooEventEntity> query = _context.FooGooEvents;

            if (minSequenceId != null) query = query.Where(x => x.SequenceId > minSequenceId);
            if (maxSequenceId != null) query = query.Where(x => x.SequenceId <= maxSequenceId);

            var items = await query.ToListAsync();
            var result = _mapper.Map<List<FooGooEventDto>>(items);
            return result;
        }

        public async Task AddFooGooEvents(List<FooGooEventDto> items)
        {
            var events = _mapper.Map<List<FooGooEventEntity>>(items);
            await _context.FooGooEvents.AddRangeAsync(events);
            await _context.SaveChangesAsync();
        }
    }
}
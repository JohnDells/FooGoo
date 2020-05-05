using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace FooGooEf
{
    public interface IFooGooDbContext
    {
        DbSet<FooGooEventEntity> FooGooEvents { get; set; }
        DbSet<FooGooSnapshotEntity> FooGooSnapshots { get; set; }
        DbSet<FooTypeEntity> FooTypes { get; set; }
        DbSet<FooEntity> Foos { get; set; }
        DbSet<BarEntity> Bars { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
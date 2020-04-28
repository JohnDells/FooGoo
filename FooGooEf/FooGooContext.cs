using Microsoft.EntityFrameworkCore;

namespace FooGooEf
{
    public class FooGooContext : DbContext, IFooGooDbContext
    {
        private readonly string _connectionString;

        public FooGooContext(string connectionString)
        {
            _connectionString = connectionString;
        }

        public DbSet<FooTypeEntity> FooTypes { get; set; }
        public DbSet<FooEntity> Foos { get; set; }
        public DbSet<BarEntity> Bars { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
        }
    }
}
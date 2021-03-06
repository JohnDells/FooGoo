﻿using Microsoft.EntityFrameworkCore;

namespace FooGooEf
{
    public class FooGooContext : DbContext, IFooGooDbContext
    {
        private readonly string _connectionString;

        public FooGooContext(string connectionString)
        {
            _connectionString = connectionString;
        }

        public FooGooContext(DbContextOptions<FooGooContext> options) : base(options)
        {
        }

        public DbSet<FooGooEventEntity> FooGooEvents { get; set; }
        public DbSet<FooGooSnapshotEntity> FooGooSnapshots { get; set; }
        public DbSet<FooTypeEntity> FooTypes { get; set; }
        public DbSet<FooEntity> Foos { get; set; }
        public DbSet<BarEntity> Bars { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(_connectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new FooGooSnapshotEntityTypeConfiguration());
            builder.ApplyConfiguration(new FooGooEventEntityTypeConfiguration());
            builder.ApplyConfiguration(new FooTypeEntityTypeConfiguration());
            builder.ApplyConfiguration(new FooEntityTypeConfiguration());
            builder.ApplyConfiguration(new BarEntityTypeConfiguration());

        }
    }
}
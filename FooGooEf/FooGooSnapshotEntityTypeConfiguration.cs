using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FooGooEf
{
    public class FooGooSnapshotEntityTypeConfiguration : IEntityTypeConfiguration<FooGooSnapshotEntity>
    {
        public void Configure(EntityTypeBuilder<FooGooSnapshotEntity> builder)
        {
            builder.ToTable("FooGooSnapshot");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedNever();
        }
    }
}
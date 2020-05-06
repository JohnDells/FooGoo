using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FooGooEf
{
    public class FooGooEventEntityTypeConfiguration : IEntityTypeConfiguration<FooGooEventEntity>
    {
        public void Configure(EntityTypeBuilder<FooGooEventEntity> builder)
        {
            builder.ToTable("FooGooEvent");
            builder.HasKey(x => x.SequenceId);
        }
    }
}
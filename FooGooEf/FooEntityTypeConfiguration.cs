using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FooGooEf
{
    public class FooEntityTypeConfiguration : IEntityTypeConfiguration<FooEntity>
    {
        public void Configure(EntityTypeBuilder<FooEntity> builder)
        {
            builder.ToTable("Foos");
            builder.HasKey(x => x.FooId);
            builder.Property(x => x.FooId).ValueGeneratedNever();
        }
    }
}
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FooGooEf
{
    public class BarEntityTypeConfiguration : IEntityTypeConfiguration<BarEntity>
    {
        public void Configure(EntityTypeBuilder<BarEntity> builder)
        {
            builder.ToTable("Bars");
            builder.HasKey(x => x.BarId);
            builder.Property(x => x.BarId).ValueGeneratedNever();
        }
    }
}
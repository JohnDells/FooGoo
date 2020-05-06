using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FooGooEf
{
    public class FooTypeEntityTypeConfiguration : IEntityTypeConfiguration<FooTypeEntity>
    {
        public void Configure(EntityTypeBuilder<FooTypeEntity> builder)
        {
            builder.ToTable("FooTypes");
            builder.HasKey(x => x.FooTypeId);
            builder.Property(x => x.FooTypeId).ValueGeneratedNever();
        }
    }
}
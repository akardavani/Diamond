using Diamond.Domain.Entities.TsePublic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Diamond.Persistance.Configuration.TsePublic
{
    internal class TseSubSectorModelConfiguration : IEntityTypeConfiguration<TseSubSector>
    {
        public void Configure(EntityTypeBuilder<TseSubSector> builder)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }
            builder.HasKey(x => x.SubSectorCode);
            builder.Property(x => x.SubSectorCode).ValueGeneratedNever();

        }
    }
}

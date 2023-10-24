using Diamond.Domain.Entities.TsePublic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Diamond.Persistance.Configuration.TsePublic
{
    internal class TseInstrumentsModelConfiguration : IEntityTypeConfiguration<TseInstrument>
    {
        public void Configure(EntityTypeBuilder<TseInstrument> builder)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }
            builder.HasKey(x => x.InsCode);
            builder.Property(x=>x.InsCode).ValueGeneratedNever();

        }
    }
}

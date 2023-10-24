using Diamond.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Diamond.Persistence.Configuration
{
    internal class InstrumentModelConfiguration : IEntityTypeConfiguration<Instrument>
    {
        public void Configure(EntityTypeBuilder<Instrument> builder)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
                //builder.ToTable("Account", "Acc");
            }
            //builder.HasKey(x => x.Id);
            builder.Property(e => e.Id)
            //.HasMaxLength(50)
            .IsUnicode(false);
        }
    }
}

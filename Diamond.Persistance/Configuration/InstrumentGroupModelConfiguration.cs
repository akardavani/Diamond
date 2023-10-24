using Diamond.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Diamond.Persistence.Configuration
{
    internal class InstrumentGroupModelConfiguration : IEntityTypeConfiguration<InstrumentGroup>
    {
        public void Configure(EntityTypeBuilder<InstrumentGroup> builder)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }
            builder.HasKey(x => x.Id);

        }
    }
}

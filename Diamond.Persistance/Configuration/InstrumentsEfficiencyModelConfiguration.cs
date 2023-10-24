using Diamond.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Diamond.Persistence.Configuration
{
    internal class InstrumentsEfficiencyModelConfiguration : IEntityTypeConfiguration<InstrumentsEfficiency>
    {
        public void Configure(EntityTypeBuilder<InstrumentsEfficiency> builder)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }
            builder.HasKey(x => x.Id);

        }
    }

}

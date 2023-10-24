using Diamond.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Diamond.Persistence.Configuration
{
    internal class MarketSegmentModelConfiguration : IEntityTypeConfiguration<MarketSegment>
    {
        public void Configure(EntityTypeBuilder<MarketSegment> builder)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }
            builder.HasKey(x => x.Id);

        }
    }
}

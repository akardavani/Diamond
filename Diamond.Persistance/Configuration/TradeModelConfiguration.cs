using Diamond.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Diamond.Persistence.Configuration
{
    internal class TradeModelConfiguration : IEntityTypeConfiguration<Trade>
    {
        public void Configure(EntityTypeBuilder<Trade> builder)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }
            builder.HasKey(x => x.Id);

        }
    }
}

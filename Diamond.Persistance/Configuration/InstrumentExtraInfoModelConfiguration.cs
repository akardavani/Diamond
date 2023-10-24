using Diamond.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Diamond.Persistance.Configuration
{
    internal class InstrumentExtraInfoModelConfiguration : IEntityTypeConfiguration<InstrumentExtraInfo>
    {
        public void Configure(EntityTypeBuilder<InstrumentExtraInfo> builder)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }
            builder.HasKey(x => x.Id);

        }
    }
}

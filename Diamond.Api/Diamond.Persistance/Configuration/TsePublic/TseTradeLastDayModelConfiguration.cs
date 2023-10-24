using Diamond.Domain.Entities.TsePublic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Diamond.Persistance.Configuration.TsePublic
{
    internal class TseTradeLastDayModelConfiguration : IEntityTypeConfiguration<TseTrade>
    {
        public void Configure(EntityTypeBuilder<TseTrade> builder)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }
            builder.HasKey(x => x.InsCode);
            builder.Property(x => x.InsCode).ValueGeneratedNever();

        }
    }
}

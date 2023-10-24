using Diamond.Domain.Entities.TsePublic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Diamond.Persistance.Configuration.TsePublic
{
    internal class TseShareChangeModelConfiguration : IEntityTypeConfiguration<TseShareChange>
    {
        public void Configure(EntityTypeBuilder<TseShareChange> builder)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }
            builder.HasKey(x => x.InsCode);

        }
    }
}

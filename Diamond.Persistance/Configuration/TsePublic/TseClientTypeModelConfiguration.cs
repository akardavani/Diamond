using Diamond.Domain.Entities.TsePublic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Diamond.Persistance.Configuration.TsePublic
{
    internal class TseClientTypeModelConfiguration : IEntityTypeConfiguration<TseClientType>
    {
        public void Configure(EntityTypeBuilder<TseClientType> builder)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }
            builder.HasKey(x => x.InsCode);

        }
    }
}

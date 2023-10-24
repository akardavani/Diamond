using Diamond.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diamond.Persistance.Configuration
{
    internal class CandelModelConfiguration : IEntityTypeConfiguration<Candel>
    {
        public void Configure(EntityTypeBuilder<Candel> builder)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }
            builder.HasKey(x => x.Id);

        }
    }
}

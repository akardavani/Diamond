using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Diamond.Domain.Entities;

namespace Diamond.Persistance.Configuration
{
    internal class CandelConfiguration : IEntityTypeConfiguration<Candel>
    {
        public void Configure(EntityTypeBuilder<Candel> builder)
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
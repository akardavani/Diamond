using Diamond.Domain;
using Diamond.Domain.Models.Identity;
using Diamond.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Context
{
    public class DiamondDbContext : IdentityDbContext<User,Role>, IDbContext
    {
        public DiamondDbContext(DbContextOptions<DiamondDbContext> options): base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(DiamondDbContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}

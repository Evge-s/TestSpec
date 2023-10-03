using Microsoft.EntityFrameworkCore;
using Server.Net.Identity.Domain.Models.UserAggregate;
using SmartEnum.EFCore;

namespace Server.Net.Identity.Infrastructure.DataBase
{
    public class IdentityDbContext : DbContext
    {
        public DbSet<User> Users { get; set; } = null!;

        public DbSet<RefreshToken> RefreshTokens { get; set; } = null!;

        public DbSet<ExternalProvider> ExternalProviders { get; set; } = null!;

        public IdentityDbContext(DbContextOptions<IdentityDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ConfigureSmartEnum();

            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new RefreshTokenConfiguration());            
            modelBuilder.ApplyConfiguration(new ExternalProviderConfiguration());            
        }
    }
}

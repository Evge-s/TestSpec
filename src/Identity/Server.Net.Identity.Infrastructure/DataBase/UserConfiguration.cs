using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Server.Net.Identity.Domain.Models.UserAggregate;

namespace Server.Net.Identity.Infrastructure.DataBase
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(u => u.Id);

            builder.Property(u => u.UserEmail)
                .HasMaxLength(128)
                .IsRequired(false);

            builder.HasIndex(u => u.UserEmail).IsUnique();

            builder.Property(u => u.DisplayName)
                .HasMaxLength(128)
                .IsRequired(false);

            builder.Property(u => u.FirstName)
                .HasMaxLength(64)
                .IsRequired(false);

            builder.Property(u => u.LastName)
                .HasMaxLength(64)
                .IsRequired(false);

            builder.Property(u => u.PasswordHash)
                .IsRequired(false);

            builder.Property(u => u.PasswordSalt)
                .IsRequired(false);

            builder.Property(u => u.Image)
                .IsRequired(false);

            // Refresh Tokens
            builder.HasMany(u => u.RefreshTokens)
                .WithOne()
                .OnDelete(DeleteBehavior.Cascade);

            // External Providers
            builder.HasMany(u => u.externalProviders)
                .WithOne()
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}

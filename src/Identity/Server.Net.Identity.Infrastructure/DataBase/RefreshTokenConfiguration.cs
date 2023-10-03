using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Server.Net.Identity.Domain.Models.UserAggregate;

namespace Server.Net.Identity.Infrastructure.DataBase
{
    public class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshToken>
    {
        public void Configure(EntityTypeBuilder<RefreshToken> builder)
        {
            builder.HasKey(ep => new { ep.Token, ep.UserId });

            builder.HasOne(ep => ep.User)
            .WithMany(u => u.RefreshTokens)
            .HasForeignKey(ep => ep.UserId);
        }
    }
}

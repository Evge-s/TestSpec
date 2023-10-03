using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Server.Net.Identity.Domain.Models.UserAggregate;

namespace Server.Net.Identity.Infrastructure.DataBase
{
    public class ExternalProviderConfiguration : IEntityTypeConfiguration<ExternalProvider>
    {
        public void Configure(EntityTypeBuilder<ExternalProvider> builder)
        {
            builder.HasKey(ep => new { ep.ProviderKey, ep.UserId });

            builder.HasOne(ep => ep.User)
            .WithMany(u => u.externalProviders)
            .HasForeignKey(ep => ep.UserId);
        }
    }
}

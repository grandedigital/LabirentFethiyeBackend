using LabirentFethiye.Domain.Entities.GlobalEntities.IdentityEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LabirentFethiye.Persistence.Configurations.GlobalConfigurations.IdentityConfigurations
{
    public class UserTokenConfiguration : IEntityTypeConfiguration<AppUserToken>
    {
        public void Configure(EntityTypeBuilder<AppUserToken> builder)
        {
            builder.HasKey(t => new { t.UserId, t.LoginProvider, t.Name });
            builder.Property(t => t.LoginProvider).HasMaxLength(150);
            builder.Property(t => t.Name).HasMaxLength(150);
            builder.ToTable("IdentityUserTokens");
        }
    }
}

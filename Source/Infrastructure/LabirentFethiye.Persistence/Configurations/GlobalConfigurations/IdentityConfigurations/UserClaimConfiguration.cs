using LabirentFethiye.Domain.Entities.GlobalEntities.IdentityEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LabirentFethiye.Persistence.Configurations.GlobalConfigurations.IdentityConfigurations
{
    public class UserClaimConfiguration : IEntityTypeConfiguration<AppUserClaim>
    {
        public void Configure(EntityTypeBuilder<AppUserClaim> builder)
        {
            builder.HasKey(uc => uc.Id);
            builder.ToTable("IdentityUserClaims");
        }
    }
}

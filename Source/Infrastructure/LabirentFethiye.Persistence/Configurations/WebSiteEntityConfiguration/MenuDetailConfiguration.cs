using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using LabirentFethiye.Domain.Entities.WebSiteEntities;
using LabirentFethiye.Persistence.Configurations.GlobalConfigurations.BaseConfigurations;

namespace LabirentFethiye.Persistence.Configurations.WebSiteEntityConfiguration
{
    public class MenuDetailConfiguration : BaseConfiguration<MenuDetail>
    {
        public override void Configure(EntityTypeBuilder<MenuDetail> builder)
        {
            base.ConfigureBase(builder);
            builder.ToTable(nameof(MenuDetail));

            builder.Property(p => p.LanguageCode).IsRequired().HasDefaultValue("tr").HasColumnOrder(ColumnOrder);
            builder.Property(p => p.Name).IsRequired().HasMaxLength(100).HasColumnOrder(ColumnOrder);
            
        }
    }
}

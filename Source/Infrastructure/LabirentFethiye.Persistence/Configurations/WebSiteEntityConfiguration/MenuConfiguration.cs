using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using LabirentFethiye.Domain.Entities.WebSiteEntities;
using LabirentFethiye.Persistence.Configurations.GlobalConfigurations.BaseConfigurations;

namespace LabirentFethiye.Persistence.Configurations.WebSiteEntityConfiguration
{
    public class MenuConfiguration : WebSiteBaseConfiguration<Menu>
    {
        public override void Configure(EntityTypeBuilder<Menu> builder)
        {
            base.ConfigureBase(builder);
            builder.ToTable(nameof(Menu));

            builder.Property(p => p.PageType).IsRequired().HasColumnOrder(ColumnOrder);
        }
    }
}

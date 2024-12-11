using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using LabirentFethiye.Domain.Entities.WebSiteEntities;
using LabirentFethiye.Persistence.Configurations.GlobalConfigurations.BaseConfigurations;

namespace LabirentFethiye.Persistence.Configurations.WebSiteEntityConfiguration
{
    public class WebPageDetailConfiguration : BaseConfiguration<WebPageDetail>
    {
        public override void Configure(EntityTypeBuilder<WebPageDetail> builder)
        {
            base.ConfigureBase(builder);
            builder.ToTable(nameof(WebPageDetail));

            builder.Property(p => p.LanguageCode).IsRequired().HasMaxLength(5).HasColumnOrder(ColumnOrder);
            builder.Property(p => p.Title).IsRequired().HasMaxLength(200).HasColumnOrder(ColumnOrder);
            builder.Property(p => p.DescriptionShort).HasMaxLength(400).HasColumnOrder(ColumnOrder);
            builder.Property(p => p.DescriptionLong).HasMaxLength(5000).HasColumnOrder(ColumnOrder);
        }
    }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using LabirentFethiye.Domain.Entities.WebSiteEntities;
using LabirentFethiye.Persistence.Configurations.GlobalConfigurations.BaseConfigurations;

namespace LabirentFethiye.Persistence.Configurations.WebSiteEntityConfiguration
{
    public class WebPhotoConfiguration : BaseConfiguration<WebPhoto>
    {
        public override void Configure(EntityTypeBuilder<WebPhoto> builder)
        {
            base.ConfigureBase(builder);
            builder.ToTable(nameof(WebPhoto));

            builder.Property(p => p.Title).HasMaxLength(100).HasColumnOrder(ColumnOrder);
            builder.Property(p => p.ImgAlt).HasMaxLength(100).HasColumnOrder(ColumnOrder);
            builder.Property(p => p.ImgTitle).HasMaxLength(100).HasColumnOrder(ColumnOrder);
            builder.Property(p => p.Image).IsRequired().HasMaxLength(200).HasColumnOrder(ColumnOrder);
            builder.Property(p => p.VideoLink).HasMaxLength(300).HasColumnOrder(ColumnOrder);
            builder.Property(p => p.Line).IsRequired().HasColumnOrder(ColumnOrder);
        }
    }
}

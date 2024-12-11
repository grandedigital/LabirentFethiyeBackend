using LabirentFethiye.Domain.Entities.ProjectEntities;
using LabirentFethiye.Persistence.Configurations.GlobalConfigurations.BaseConfigurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LabirentFethiye.Persistence.Configurations.ProjectConfigurations
{
    public class HotelDetailConfiguration : BaseConfiguration<HotelDetail>
    {
        public override void Configure(EntityTypeBuilder<HotelDetail> builder)
        {
            base.ConfigureBase(builder);
            builder.ToTable(nameof(HotelDetail));

            builder.Property(p => p.LanguageCode).IsRequired().HasMaxLength(5).HasColumnOrder(ColumnOrder);
            builder.Property(p => p.Name).IsRequired().HasMaxLength(100).HasColumnOrder(ColumnOrder);
            builder.Property(p => p.DescriptionShort).IsRequired(false).HasMaxLength(400).HasColumnOrder(ColumnOrder);
            builder.Property(p => p.DescriptionLong).IsRequired(false).HasMaxLength(5000).HasColumnOrder(ColumnOrder);

            builder.Property(p => p.FeatureTextBlue).IsRequired(false).HasMaxLength(100).HasColumnOrder(ColumnOrder);
            builder.Property(p => p.FeatureTextRed).IsRequired(false).HasMaxLength(100).HasColumnOrder(ColumnOrder);
            builder.Property(p => p.FeatureTextWhite).IsRequired(false).HasMaxLength(100).HasColumnOrder(ColumnOrder);

            builder
                .HasOne(p => p.Hotel)
                .WithMany(c => c.HotelDetails)
                .HasForeignKey(p => p.HotelId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }

}

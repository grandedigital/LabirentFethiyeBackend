using LabirentFethiye.Domain.Entities.ProjectEntities;
using LabirentFethiye.Persistence.Configurations.GlobalConfigurations.BaseConfigurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LabirentFethiye.Persistence.Configurations.ProjectConfigurations
{
    public class RoomDetailConfiguration : BaseConfiguration<RoomDetail>
    {
        public override void Configure(EntityTypeBuilder<RoomDetail> builder)
        {
            base.ConfigureBase(builder);
            builder.ToTable(nameof(RoomDetail));

            builder.Property(p => p.LanguageCode).IsRequired().HasMaxLength(5).HasColumnOrder(ColumnOrder);
            builder.Property(p => p.Name).IsRequired().HasMaxLength(100).HasColumnOrder(ColumnOrder);
            builder.Property(p => p.DescriptionShort).IsRequired(false).HasMaxLength(400).HasColumnOrder(ColumnOrder);
            builder.Property(p => p.DescriptionLong).IsRequired(false).HasMaxLength(5000).HasColumnOrder(ColumnOrder);

            builder.Property(p => p.FeatureTextBlue).IsRequired(false).HasMaxLength(100).HasColumnOrder(ColumnOrder);
            builder.Property(p => p.FeatureTextRed).IsRequired(false).HasMaxLength(100).HasColumnOrder(ColumnOrder);
            builder.Property(p => p.FeatureTextWhite).IsRequired(false).HasMaxLength(100).HasColumnOrder(ColumnOrder);

            builder
                .HasOne(p => p.Room)
                .WithMany(c => c.RoomDetails)
                .HasForeignKey(p => p.RoomId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}

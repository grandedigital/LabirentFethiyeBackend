using LabirentFethiye.Domain.Entities.ProjectEntities;
using LabirentFethiye.Persistence.Configurations.GlobalConfigurations.BaseConfigurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LabirentFethiye.Persistence.Configurations.ProjectConfigurations
{
    public class PhotoConfiguration : BaseConfiguration<Photo>
    {
        public override void Configure(EntityTypeBuilder<Photo> builder)
        {
            base.ConfigureBase(builder);
            builder.ToTable(nameof(Photo));

            builder.Property(p => p.Title).IsRequired(false).HasMaxLength(100).HasColumnOrder(ColumnOrder);
            builder.Property(p => p.ImgAlt).IsRequired(false).HasMaxLength(100).HasColumnOrder(ColumnOrder);
            builder.Property(p => p.ImgTitle).IsRequired(false).HasMaxLength(100).HasColumnOrder(ColumnOrder);
            builder.Property(p => p.Image).IsRequired(false).HasMaxLength(200).HasColumnOrder(ColumnOrder);
            builder.Property(p => p.VideoLink).IsRequired(false).HasMaxLength(300).HasColumnOrder(ColumnOrder);
            builder.Property(p => p.Line).HasDefaultValue(0).HasColumnOrder(ColumnOrder);

            builder
                .HasOne(p => p.Villa)
                .WithMany(c => c.Photos)
                .HasForeignKey(p => p.VillaId)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired(false);

            builder
                .HasOne(p => p.Hotel)
                .WithMany(c => c.Photos)
                .HasForeignKey(p => p.HotelId)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired(false);

            builder
                .HasOne(p => p.Room)
                .WithMany(c => c.Photos)
                .HasForeignKey(p => p.RoomId)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired(false);
        }
    }
}

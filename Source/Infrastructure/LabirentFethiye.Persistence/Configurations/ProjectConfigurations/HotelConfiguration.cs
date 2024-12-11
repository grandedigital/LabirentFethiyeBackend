using LabirentFethiye.Common.Enums;
using LabirentFethiye.Domain.Entities.ProjectEntities;
using LabirentFethiye.Persistence.Configurations.GlobalConfigurations.BaseConfigurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LabirentFethiye.Persistence.Configurations.ProjectConfigurations
{
    public class HotelConfiguration : BaseConfiguration<Hotel>
    {
        public override void Configure(EntityTypeBuilder<Hotel> builder)
        {
            base.ConfigureBase(builder);
            builder.ToTable(nameof(Hotel));

            builder.Property(p => p.Room).HasDefaultValue(0).HasColumnOrder(ColumnOrder);
            builder.Property(p => p.Person).HasDefaultValue(0).HasColumnOrder(ColumnOrder);
            builder.Property(p => p.Bath).HasDefaultValue(0).HasColumnOrder(ColumnOrder);

            builder.Property(p => p.GoogleMap).HasMaxLength(350).IsRequired(false).HasColumnOrder(ColumnOrder);
            builder.Property(p => p.WaterMaterNumber).HasMaxLength(50).IsRequired(false).HasColumnOrder(ColumnOrder);
            builder.Property(p => p.ElectricityMeterNumber).HasMaxLength(50).IsRequired(false).HasColumnOrder(ColumnOrder);
            builder.Property(p => p.InternetMeterNumber).HasMaxLength(50).IsRequired(false).HasColumnOrder(ColumnOrder);
            builder.Property(p => p.WifiPassword).HasMaxLength(50).IsRequired(false).HasColumnOrder(ColumnOrder);
            builder.Property(p => p.PriceType).IsRequired().HasDefaultValue(PriceType.TL).HasColumnOrder(ColumnOrder);

            builder.Property(p => p.Line).HasDefaultValue(0).HasColumnOrder(ColumnOrder);

            builder
                .HasOne(p => p.Town)
                .WithMany(c => c.Hotels)
                .HasForeignKey(p => p.TownId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}

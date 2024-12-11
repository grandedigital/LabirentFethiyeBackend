using LabirentFethiye.Common.Enums;
using LabirentFethiye.Domain.Entities.ProjectEntities;
using LabirentFethiye.Persistence.Configurations.GlobalConfigurations.BaseConfigurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LabirentFethiye.Persistence.Configurations.ProjectConfigurations
{
    public class RoomConfiguration : BaseConfiguration<Room>
    {
        public override void Configure(EntityTypeBuilder<Room> builder)
        {
            base.ConfigureBase(builder);
            builder.ToTable(nameof(Room));

            builder.Property(p => p.Rooms).HasDefaultValue(0).HasColumnOrder(ColumnOrder);
            builder.Property(p => p.Person).HasDefaultValue(0).HasColumnOrder(ColumnOrder);
            builder.Property(p => p.Bath).HasDefaultValue(0).HasColumnOrder(ColumnOrder);

            builder.Property(p => p.WaterMaterNumber).HasMaxLength(50).IsRequired(false).HasColumnOrder(ColumnOrder);
            builder.Property(p => p.ElectricityMeterNumber).HasMaxLength(50).IsRequired(false).HasColumnOrder(ColumnOrder);
            builder.Property(p => p.InternetMeterNumber).HasMaxLength(50).IsRequired(false).HasColumnOrder(ColumnOrder);
            builder.Property(p => p.WifiPassword).HasMaxLength(50).IsRequired(false).HasColumnOrder(ColumnOrder);
            builder.Property(p => p.Line).HasDefaultValue(0).HasColumnOrder(ColumnOrder);
            builder.Property(p => p.PriceType).IsRequired().HasDefaultValue(PriceType.TL).HasColumnOrder(ColumnOrder);

            builder.Property(p => p.OnlineReservation).HasDefaultValue(false).HasColumnOrder(ColumnOrder);

            builder
                .HasOne(p => p.Hotel)
                .WithMany(c => c.Rooms)
                .HasForeignKey(p => p.HotelId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}

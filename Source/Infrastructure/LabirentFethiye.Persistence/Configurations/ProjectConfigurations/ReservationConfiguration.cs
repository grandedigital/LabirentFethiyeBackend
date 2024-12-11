using LabirentFethiye.Common.Enums;
using LabirentFethiye.Domain.Entities.ProjectEntities;
using LabirentFethiye.Persistence.Configurations.GlobalConfigurations.BaseConfigurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LabirentFethiye.Persistence.Configurations.ProjectConfigurations
{
    public class ReservationConfiguration : BaseConfiguration<Reservation>
    {
        public override void Configure(EntityTypeBuilder<Reservation> builder)
        {
            base.ConfigureBase(builder);
            builder.ToTable(nameof(Reservation));

            builder.Property(p => p.ReservationNumber).IsRequired().HasMaxLength(70).HasColumnOrder(ColumnOrder);
            builder.Property(p => p.ReservationStatusType).IsRequired().HasDefaultValue(ReservationStatusType.Reserved).HasColumnOrder(ColumnOrder);
            builder.Property(p => p.ReservationChannalType).IsRequired().HasDefaultValue(ReservationChannalType.HomeOwner).HasColumnOrder(ColumnOrder);

            builder.Property(p => p.Note).IsRequired(false).HasMaxLength(500).HasColumnOrder(ColumnOrder);
            builder.Property(p => p.CheckIn).IsRequired().HasColumnOrder(ColumnOrder);
            builder.Property(p => p.CheckOut).IsRequired().HasColumnOrder(ColumnOrder);
            builder.Property(p => p.IsDepositPrice).HasDefaultValue(false).HasColumnOrder(ColumnOrder);
            builder.Property(p => p.IsCleaningPrice).HasDefaultValue(false).HasColumnOrder(ColumnOrder);
            builder.Property(p => p.Amount).IsRequired().HasColumnType("decimal(10,2)").HasColumnOrder(ColumnOrder);
            builder.Property(p => p.Discount).IsRequired().HasDefaultValue(0).HasColumnType("decimal(10,2)").HasColumnOrder(ColumnOrder);
            builder.Property(p => p.ExtraPrice).IsRequired().HasColumnType("decimal(10,2)").HasColumnOrder(ColumnOrder);
            builder.Property(p => p.Total).IsRequired().HasColumnType("decimal(10,2)").HasColumnOrder(ColumnOrder);
            builder.Property(p => p.PriceType).IsRequired().HasDefaultValue(PriceType.TL).HasColumnOrder(ColumnOrder);
            builder.Property(p => p.HomeOwner).HasDefaultValue(false).HasColumnOrder(ColumnOrder);

            builder
                .HasOne(x => x.Villa)
                .WithMany(x => x.Reservations)
                .HasForeignKey(x => x.VillaId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasOne(x => x.Room)
                .WithMany(x => x.Reservations)
                .HasForeignKey(x => x.RoomId)
                .OnDelete(DeleteBehavior.Restrict);
                //.IsRequired(false);
        }
    }
}

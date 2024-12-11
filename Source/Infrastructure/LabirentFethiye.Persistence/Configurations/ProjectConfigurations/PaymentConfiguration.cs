using LabirentFethiye.Common.Enums;
using LabirentFethiye.Domain.Entities.ProjectEntities;
using LabirentFethiye.Persistence.Configurations.GlobalConfigurations.BaseConfigurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LabirentFethiye.Persistence.Configurations.ProjectConfigurations
{
    public class PaymentConfiguration : BaseConfiguration<Payment>
    {
        public override void Configure(EntityTypeBuilder<Payment> builder)
        {
            base.ConfigureBase(builder);
            builder.ToTable(nameof(Payment));

            builder.Property(p => p.Amount).IsRequired().HasColumnType("decimal(18, 2)").HasColumnOrder(ColumnOrder);
            builder.Property(p => p.Description).IsRequired(false).HasMaxLength(400).HasColumnOrder(ColumnOrder);
            builder.Property(p => p.InOrOut).IsRequired().HasColumnOrder(ColumnOrder);
            builder.Property(p => p.PriceType).IsRequired().HasDefaultValue(PriceType.TL).HasColumnOrder(ColumnOrder);

            builder
               .HasOne(p => p.PaymentType)
               .WithMany(c => c.Payments)
               .HasForeignKey(p => p.PaymentTypeId)
               .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasOne(p => p.Villa)
                .WithMany(c => c.Payments)
                .HasForeignKey(p => p.VillaId)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired(false);

            builder
                .HasOne(p => p.Hotel)
                .WithMany(c => c.Payments)
                .HasForeignKey(p => p.HotelId)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired(false);

            builder
                .HasOne(p => p.Room)
                .WithMany(c => c.Payments)
                .HasForeignKey(p => p.RoomId)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired(false);

        }
    }
}

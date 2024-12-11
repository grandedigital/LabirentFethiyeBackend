using LabirentFethiye.Common.Enums;
using LabirentFethiye.Domain.Entities.ProjectEntities;
using LabirentFethiye.Persistence.Configurations.GlobalConfigurations.BaseConfigurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LabirentFethiye.Persistence.Configurations.ProjectConfigurations
{
    public class VillaConfiguration : BaseConfiguration<Villa>
    {
        public override void Configure(EntityTypeBuilder<Villa> builder)
        {
            base.ConfigureBase(builder);
            builder.ToTable(nameof(Villa));

            builder.Property(p => p.Room).HasDefaultValue(0).HasColumnOrder(ColumnOrder);
            builder.Property(p => p.Bath).HasDefaultValue(0).HasColumnOrder(ColumnOrder);
            builder.Property(p => p.Person).HasDefaultValue(0).HasColumnOrder(ColumnOrder);
            builder.Property(p => p.GoogleMap).IsRequired(false).HasMaxLength(300).HasColumnOrder(ColumnOrder);
            builder.Property(p => p.OnlineReservation).HasDefaultValue(false).HasColumnOrder(ColumnOrder);
            builder.Property(p => p.IsRent).HasDefaultValue(true).HasColumnOrder(ColumnOrder);
            builder.Property(p => p.IsSale).HasDefaultValue(false).HasColumnOrder(ColumnOrder);

            builder.Property(p => p.WaterMaterNumber).IsRequired(false).HasMaxLength(50).IsRequired(false).HasColumnOrder(ColumnOrder);
            builder.Property(p => p.ElectricityMeterNumber).IsRequired(false).HasMaxLength(50).IsRequired(false).HasColumnOrder(ColumnOrder);
            builder.Property(p => p.InternetMeterNumber).IsRequired(false).HasMaxLength(50).IsRequired(false).HasColumnOrder(ColumnOrder);
            builder.Property(p => p.WifiPassword).IsRequired(false).HasMaxLength(50).IsRequired(false).HasColumnOrder(ColumnOrder);

            builder.Property(p => p.VillaNumber).IsRequired().HasMaxLength(20).HasColumnOrder(ColumnOrder);
            builder.Property(p => p.VillaOwnerName).IsRequired(false).HasMaxLength(150).IsRequired(false).HasColumnOrder(ColumnOrder);
            builder.Property(p => p.VillaOwnerPhone).IsRequired(false).HasMaxLength(50).IsRequired(false).HasColumnOrder(ColumnOrder);
            builder.Property(p => p.PriceType).IsRequired().HasDefaultValue(PriceType.TL).HasColumnOrder(ColumnOrder);

            builder.Property(p => p.Line).HasDefaultValue(0).HasColumnOrder(ColumnOrder);
            builder.Property(p => p.MinimumReservationNight).HasDefaultValue(5).HasColumnOrder(ColumnOrder);

            builder.HasIndex(v => v.VillaNumber).IsUnique();

            builder
                .HasOne(p => p.Town)
                .WithMany(c => c.Villas)
                .HasForeignKey(p => p.TownId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasOne(v => v.Personal)
                .WithMany(u=>u.Villas)
                .HasForeignKey(p=>p.PersonalId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}

using LabirentFethiye.Domain.Entities.ProjectEntities;
using LabirentFethiye.Persistence.Configurations.GlobalConfigurations.BaseConfigurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LabirentFethiye.Persistence.Configurations.ProjectConfigurations
{
    public class ReservationItemConfiguration : BaseConfiguration<ReservationItem>
    {
        public override void Configure(EntityTypeBuilder<ReservationItem> builder)
        {
            base.ConfigureBase(builder);
            builder.ToTable(nameof(ReservationItem));

            builder.Property(p => p.Day).IsRequired().HasColumnOrder(ColumnOrder);
            builder.Property(p => p.Price).IsRequired().HasColumnType("decimal(10,2)").HasColumnOrder(ColumnOrder);

            builder
                .HasOne(x => x.Reservation)
                .WithMany(x => x.ReservationItems)
                .HasForeignKey(x => x.ReservationId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}

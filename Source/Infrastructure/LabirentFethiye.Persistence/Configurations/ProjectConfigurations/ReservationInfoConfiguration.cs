using LabirentFethiye.Common.Enums;
using LabirentFethiye.Domain.Entities.ProjectEntities;
using LabirentFethiye.Persistence.Configurations.GlobalConfigurations.BaseConfigurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LabirentFethiye.Persistence.Configurations.ProjectConfigurations
{
    public class ReservationInfoConfiguration : BaseConfiguration<ReservationInfo>
    {
        public override void Configure(EntityTypeBuilder<ReservationInfo> builder)
        {
            base.ConfigureBase(builder);
            builder.ToTable(nameof(ReservationInfo));

            builder.Property(p => p.Name).IsRequired().HasMaxLength(50).HasColumnOrder(ColumnOrder);
            builder.Property(p => p.Surname).IsRequired().HasMaxLength(50).HasColumnOrder(ColumnOrder);
            builder.Property(p => p.Phone).IsRequired(false).HasMaxLength(50).HasColumnOrder(ColumnOrder);
            builder.Property(p => p.Email).IsRequired(false).HasMaxLength(120).HasColumnOrder(ColumnOrder);
            builder.Property(p => p.Owner).IsRequired().HasColumnOrder(ColumnOrder);
            builder.Property(p => p.PeopleType).IsRequired().HasDefaultValue(PeopleType.Adult).HasColumnOrder(ColumnOrder);

            builder
                .HasOne(x => x.Reservation)
                .WithMany(x => x.ReservationInfos)
                .HasForeignKey(x => x.ReservationId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}

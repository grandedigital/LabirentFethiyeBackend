using LabirentFethiye.Domain.Entities.ProjectEntities;
using LabirentFethiye.Persistence.Configurations.GlobalConfigurations.BaseConfigurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LabirentFethiye.Persistence.Configurations.ProjectConfigurations
{
    public class DistanceRulerConfiguration : BaseConfiguration<DistanceRuler>
    {
        public override void Configure(EntityTypeBuilder<DistanceRuler> builder)
        {
            base.ConfigureBase(builder);
            builder.ToTable(nameof(DistanceRuler));

            builder.Property(p => p.Icon).IsRequired(false).HasMaxLength(70).HasColumnOrder(ColumnOrder);
            builder.Property(p => p.Line).HasDefaultValue(0).HasColumnOrder(ColumnOrder);
            builder.Property(p => p.Value).IsRequired().HasMaxLength(50).HasColumnOrder(ColumnOrder);

            builder
                .HasOne(p => p.Villa)
                .WithMany(c => c.DistanceRulers)
                .HasForeignKey(p => p.VillaId)
                .OnDelete(DeleteBehavior.Restrict)

                .IsRequired(false);

            builder
                .HasOne(p => p.Hotel)
                .WithMany(c => c.DistanceRulers)
                .HasForeignKey(p => p.HotelId)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired(false);
        }
    }
}

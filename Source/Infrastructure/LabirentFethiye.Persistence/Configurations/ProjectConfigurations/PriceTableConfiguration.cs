using LabirentFethiye.Domain.Entities.ProjectEntities;
using LabirentFethiye.Persistence.Configurations.GlobalConfigurations.BaseConfigurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LabirentFethiye.Persistence.Configurations.ProjectConfigurations
{
    public class PriceTableConfiguration : BaseConfiguration<PriceTable>
    {
        public override void Configure(EntityTypeBuilder<PriceTable> builder)
        {
            base.ConfigureBase(builder);
            builder.ToTable(nameof(PriceTable));

            builder.Property(p => p.Icon).IsRequired(false).HasMaxLength(70).HasColumnOrder(ColumnOrder);
            builder.Property(p => p.Price).IsRequired().HasColumnOrder(ColumnOrder);
            builder.Property(p => p.Line).HasDefaultValue(0).HasColumnOrder(ColumnOrder);

            builder
                .HasOne(p => p.Villa)
                .WithMany(c => c.PriceTables)
                .HasForeignKey(p => p.VillaId)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired(false);

            builder
                .HasOne(p => p.Room)
                .WithMany(c => c.PriceTables)
                .HasForeignKey(p => p.RoomId)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired(false);
        }
    }
}

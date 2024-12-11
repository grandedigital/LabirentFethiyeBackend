using LabirentFethiye.Domain.Entities.ProjectEntities;
using LabirentFethiye.Persistence.Configurations.GlobalConfigurations.BaseConfigurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LabirentFethiye.Persistence.Configurations.ProjectConfigurations
{
    public class PriceDateConfiguration : BaseConfiguration<PriceDate>
    {
        public override void Configure(EntityTypeBuilder<PriceDate> builder)
        {
            base.ConfigureBase(builder);
            builder.ToTable(nameof(PriceDate));

            builder.Property(p => p.Price).IsRequired().HasColumnType("decimal(18, 2)").HasColumnOrder(ColumnOrder);
            builder.Property(p => p.StartDate).IsRequired().HasColumnOrder(ColumnOrder);
            builder.Property(p => p.EndDate).IsRequired().HasColumnOrder(ColumnOrder);

            builder
                .HasOne(p => p.Villa)
                .WithMany(c => c.PriceDates)
                .HasForeignKey(p => p.VillaId)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired(false);

            builder
                .HasOne(p => p.Room)
                .WithMany(c => c.PriceDates)
                .HasForeignKey(p => p.RoomId)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired(false);
        }
    }
}

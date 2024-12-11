using LabirentFethiye.Domain.Entities.ProjectEntities;
using LabirentFethiye.Persistence.Configurations.GlobalConfigurations.BaseConfigurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LabirentFethiye.Persistence.Configurations.ProjectConfigurations
{
    public class PriceTableDetailConfiguration : BaseConfiguration<PriceTableDetail>
    {
        public override void Configure(EntityTypeBuilder<PriceTableDetail> builder)
        {
            base.ConfigureBase(builder);
            builder.ToTable(nameof(PriceTableDetail));

            builder.Property(p => p.LanguageCode).IsRequired().HasMaxLength(5).HasColumnOrder(ColumnOrder);
            builder.Property(p => p.Title).IsRequired().HasMaxLength(100).HasColumnOrder(ColumnOrder);
            builder.Property(p => p.Description).IsRequired(false).HasMaxLength(500).HasColumnOrder(ColumnOrder);

            builder
                .HasOne(p => p.PriceTable)
                .WithMany(c => c.PriceTableDetails)
                .HasForeignKey(p => p.PriceTableId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}

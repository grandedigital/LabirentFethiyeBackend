using LabirentFethiye.Domain.Entities.ProjectEntities;
using LabirentFethiye.Persistence.Configurations.GlobalConfigurations.BaseConfigurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LabirentFethiye.Persistence.Configurations.ProjectConfigurations
{
    public class DistanceRulerDetailConfiguration : BaseConfiguration<DistanceRulerDetail>
    {
        public override void Configure(EntityTypeBuilder<DistanceRulerDetail> builder)
        {
            base.ConfigureBase(builder);
            builder.ToTable(nameof(DistanceRulerDetail));

            builder.Property(p => p.LanguageCode).IsRequired().HasMaxLength(5).HasColumnOrder(ColumnOrder);
            builder.Property(p => p.Name).IsRequired().HasMaxLength(50).HasColumnOrder(ColumnOrder);

            builder
                .HasOne(p => p.DistanceRuler)
                .WithMany(c => c.DistanceRulerDetails)
                .HasForeignKey(p => p.DistanceRulerId)
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}

using LabirentFethiye.Domain.Entities.ProjectEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LabirentFethiye.Persistence.Configurations.ProjectConfigurations
{
    public class VillaFeatureConfiguration : IEntityTypeConfiguration<VillaFeature>
    {
        public void Configure(EntityTypeBuilder<VillaFeature> builder)
        {
            builder.ToTable(nameof(VillaFeature));

            builder
                .HasKey(sc => new { sc.VillaId, sc.FeatureId});

            builder
                .HasOne(sc => sc.Villa)
                .WithMany(s => s.VillaFeatures)
                .HasForeignKey(sc => sc.VillaId);

            builder
                .HasOne(sc => sc.Feature)
                .WithMany(c => c.VillaFeatures)
                .HasForeignKey(sc => sc.FeatureId);
        }
    }
}

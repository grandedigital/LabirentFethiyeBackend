using LabirentFethiye.Domain.Entities.ProjectEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LabirentFethiye.Persistence.Configurations.ProjectConfigurations
{
    public class VillaCategoryConfiguration : IEntityTypeConfiguration<VillaCategory>
    {
        public void Configure(EntityTypeBuilder<VillaCategory> builder)
        {
            builder.ToTable(nameof(VillaCategory));

            builder
                .HasKey(sc => new { sc.VillaId, sc.CategoryId});

            builder
                .HasOne(sc => sc.Villa)
                .WithMany(s => s.VillaCategories)
                .HasForeignKey(sc => sc.VillaId);

            builder
                .HasOne(sc => sc.Category)
                .WithMany(c => c.VillaCategories)
                .HasForeignKey(sc => sc.CategoryId);
        }
    }
}

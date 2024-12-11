using LabirentFethiye.Domain.Entities.ProjectEntities;
using LabirentFethiye.Persistence.Configurations.GlobalConfigurations.BaseConfigurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace LabirentFethiye.Persistence.Configurations.ProjectConfigurations
{
    public class FeatureConfiguration : BaseConfiguration<Feature>
    {
        public override void Configure(EntityTypeBuilder<Feature> builder)
        {
            base.ConfigureBase(builder);
            builder.ToTable(nameof(Feature));

            builder.Property(p => p.Icon).IsRequired(false).HasMaxLength(70).HasColumnOrder(ColumnOrder);
            builder.Property(p => p.Line).HasDefaultValue(0).HasColumnOrder(ColumnOrder);

            builder
                .HasOne(f => f.Parent) // Parent ilişkisi
                .WithMany(f => f.Children) // Parent'in çocukları
                .HasForeignKey(f => f.ParentId) // Foreign key
                .OnDelete(DeleteBehavior.Restrict) // Silme davranışı
                .IsRequired(false);
        }
    }
}

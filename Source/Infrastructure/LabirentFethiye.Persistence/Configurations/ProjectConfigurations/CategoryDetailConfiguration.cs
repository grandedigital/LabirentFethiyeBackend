using LabirentFethiye.Domain.Entities.ProjectEntities;
using LabirentFethiye.Persistence.Configurations.GlobalConfigurations.BaseConfigurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LabirentFethiye.Persistence.Configurations.ProjectConfigurations
{
    public class CategoryDetailConfiguration : BaseConfiguration<CategoryDetail>
    {
        public override void Configure(EntityTypeBuilder<CategoryDetail> builder)
        {
            base.ConfigureBase(builder);
            builder.ToTable(nameof(CategoryDetail));

            builder.Property(p => p.LanguageCode).IsRequired().HasMaxLength(5).HasColumnOrder(ColumnOrder);
            builder.Property(p => p.Name).IsRequired().HasMaxLength(50).HasColumnOrder(ColumnOrder);
            builder.Property(p => p.DescriptionShort).IsRequired(false).HasMaxLength(400).HasColumnOrder(ColumnOrder);
            builder.Property(p => p.DescriptionLong).IsRequired(false).HasMaxLength(5000).HasColumnOrder(ColumnOrder);

            builder
                .HasOne(p => p.Category)
                .WithMany(c => c.CategoryDetails)
                .HasForeignKey(p => p.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}

using LabirentFethiye.Domain.Entities.ProjectEntities;
using LabirentFethiye.Persistence.Configurations.GlobalConfigurations.BaseConfigurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LabirentFethiye.Persistence.Configurations.ProjectConfigurations
{
    public class CategoryConfiguration : BaseConfiguration<Category>
    {
        public override void Configure(EntityTypeBuilder<Category> builder)
        {
            base.ConfigureBase(builder);
            builder.ToTable(nameof(Category));

            builder.Property(p => p.Icon).IsRequired(false).HasMaxLength(70).HasColumnOrder(ColumnOrder);
            builder.Property(p => p.Line).HasDefaultValue(0).HasColumnOrder(ColumnOrder);
        }
    }
}

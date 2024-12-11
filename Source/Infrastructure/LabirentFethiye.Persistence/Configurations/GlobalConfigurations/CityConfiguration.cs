using LabirentFethiye.Domain.Entities.GlobalEntities;
using LabirentFethiye.Persistence.Configurations.GlobalConfigurations.BaseConfigurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LabirentFethiye.Persistence.Configurations.GlobalConfigurations
{
    public class CityConfiguration : BaseConfiguration<City>
    {
        public override void Configure(EntityTypeBuilder<City> builder)
        {
            base.ConfigureBase(builder);
            builder.ToTable(nameof(City));

            builder.Property(p => p.Name).IsRequired().HasMaxLength(70).HasColumnOrder(ColumnOrder);
            builder.Property(p => p.CityNumber).IsRequired().HasColumnOrder(ColumnOrder);         
        }
    }
}

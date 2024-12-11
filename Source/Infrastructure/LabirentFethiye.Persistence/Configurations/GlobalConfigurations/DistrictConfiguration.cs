using LabirentFethiye.Domain.Entities.GlobalEntities;
using LabirentFethiye.Persistence.Configurations.GlobalConfigurations.BaseConfigurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LabirentFethiye.Persistence.Configurations.GlobalConfigurations
{
    public class DistrictConfiguration : BaseConfiguration<District>
    {
        public override void Configure(EntityTypeBuilder<District> builder)
        {
            base.ConfigureBase(builder);
            builder.ToTable(nameof(District));

            builder.Property(p => p.Name).IsRequired().HasMaxLength(70).HasColumnOrder(ColumnOrder);

            builder
                .HasOne(p => p.City)       
                .WithMany(c => c.Districts) 
                .HasForeignKey(p => p.CityId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}

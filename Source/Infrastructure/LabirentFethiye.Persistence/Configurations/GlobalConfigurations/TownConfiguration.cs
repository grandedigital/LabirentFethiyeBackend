using LabirentFethiye.Domain.Entities.GlobalEntities;
using LabirentFethiye.Persistence.Configurations.GlobalConfigurations.BaseConfigurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LabirentFethiye.Persistence.Configurations.GlobalConfigurations
{
    public class TownConfiguration : BaseConfiguration<Town>
    {
        public override void Configure(EntityTypeBuilder<Town> builder)
        {
            base.ConfigureBase(builder);
            builder.ToTable(nameof(Town));

            builder.Property(p => p.Name).IsRequired().HasMaxLength(70).HasColumnOrder(ColumnOrder);

            builder
                .HasOne(p => p.District)
                .WithMany(c => c.Towns)
                .HasForeignKey(p => p.DistrictId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}

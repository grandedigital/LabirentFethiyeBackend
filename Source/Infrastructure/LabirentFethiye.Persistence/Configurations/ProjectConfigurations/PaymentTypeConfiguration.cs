using LabirentFethiye.Domain.Entities.ProjectEntities;
using LabirentFethiye.Persistence.Configurations.GlobalConfigurations.BaseConfigurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LabirentFethiye.Persistence.Configurations.ProjectConfigurations
{
    public class PaymentTypeConfiguration : BaseConfiguration<PaymentType>
    {
        public override void Configure(EntityTypeBuilder<PaymentType> builder)
        {
            base.ConfigureBase(builder);
            builder.ToTable(nameof(PaymentType));

            builder.Property(p => p.Title).IsRequired().HasMaxLength(100).HasColumnOrder(ColumnOrder);
            builder.Property(p => p.Description).IsRequired(false).HasMaxLength(400).HasColumnOrder(ColumnOrder);           
        }
    }
}


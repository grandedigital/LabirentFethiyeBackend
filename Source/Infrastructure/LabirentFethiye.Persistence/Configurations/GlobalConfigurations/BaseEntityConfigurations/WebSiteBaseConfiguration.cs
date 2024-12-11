using LabirentFethiye.Common.Enums;
using LabirentFethiye.Domain.Entities.GlobalEntities.BaseEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LabirentFethiye.Persistence.Configurations.GlobalConfigurations.BaseConfigurations
{
    public class WebSiteBaseConfiguration<T> : IEntityTypeConfiguration<T> where T : WebSiteBaseEntity
    {
        private int _columnOrder = 0;
        public int ColumnOrder => ++_columnOrder;
        public virtual void Configure(EntityTypeBuilder<T> builder) { }
        public void ConfigureBase(EntityTypeBuilder<T> builder)
        {
            builder.HasKey(k => k.Id);
            builder.Property(p => p.Id).HasColumnOrder(ColumnOrder).ValueGeneratedOnAdd();

            if (builder.Property(x => x.Id) == null) builder.Property(x => x.Id).HasDefaultValue(Guid.NewGuid());

            builder.Property(p => p.CreatedById).IsRequired().HasDefaultValueSql("0x0").HasColumnOrder(ColumnOrder);
            builder.Property(p => p.CreatedAt).IsRequired().HasDefaultValueSql("GETDATE()").HasColumnOrder(ColumnOrder);
            builder.Property(p => p.UpdatedById).IsRequired().HasDefaultValueSql("0x0").HasColumnOrder(ColumnOrder);
            builder.Property(p => p.UpdatedAt).IsRequired().HasColumnOrder(ColumnOrder);
            builder.Property(p => p.GeneralStatusType).IsRequired().HasDefaultValue(GeneralStatusType.Active).HasColumnOrder(ColumnOrder);

            builder.Property(p => p.MetaDescription).IsRequired().HasMaxLength(250).HasColumnOrder(ColumnOrder);
            builder.Property(p => p.MetaTitle).IsRequired().HasMaxLength(100).HasColumnOrder(ColumnOrder);
            builder.Property(p => p.Slug).IsRequired().HasMaxLength(250).HasColumnOrder(ColumnOrder);

            builder
                .HasIndex(b => b.Slug);
        }
    }
}

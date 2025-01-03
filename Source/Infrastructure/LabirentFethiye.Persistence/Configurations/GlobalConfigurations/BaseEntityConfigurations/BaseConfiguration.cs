﻿using LabirentFethiye.Common.Enums;
using LabirentFethiye.Domain.Entities.GlobalEntities.BaseEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LabirentFethiye.Persistence.Configurations.GlobalConfigurations.BaseConfigurations
{
    public class BaseConfiguration<T> : IEntityTypeConfiguration<T> where T : BaseEntity
    {
        private int _columnOrder = 0;
        public int ColumnOrder => ++_columnOrder;
        public virtual void Configure(EntityTypeBuilder<T> builder) { }
        public void ConfigureBase(EntityTypeBuilder<T> builder)
        {
            builder.HasKey(k => k.Id);
            builder.Property(p => p.Id).HasColumnOrder(ColumnOrder).ValueGeneratedOnAdd();

            if (builder.Property(x => x.Id) == null) builder.Property(x => x.Id).HasDefaultValue(Guid.NewGuid());

            builder.Property(p => p.CreatedById).HasColumnOrder(ColumnOrder);
            builder.Property(p => p.CreatedAt).IsRequired().HasDefaultValueSql("GETDATE()").HasColumnOrder(ColumnOrder);
            builder.Property(p => p.UpdatedById).HasColumnOrder(ColumnOrder);
            builder.Property(p => p.UpdatedAt).HasColumnOrder(ColumnOrder);
            builder.Property(p => p.GeneralStatusType).IsRequired().HasDefaultValue(GeneralStatusType.Active).HasColumnOrder(ColumnOrder);
        }
    }
}

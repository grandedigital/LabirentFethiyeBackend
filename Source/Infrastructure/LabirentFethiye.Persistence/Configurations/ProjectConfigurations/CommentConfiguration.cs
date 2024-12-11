using LabirentFethiye.Domain.Entities.ProjectEntities;
using LabirentFethiye.Persistence.Configurations.GlobalConfigurations.BaseConfigurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LabirentFethiye.Persistence.Configurations.ProjectConfigurations
{
    public class CommentConfiguration : BaseConfiguration<Comment>
    {
        public override void Configure(EntityTypeBuilder<Comment> builder)
        {
            base.ConfigureBase(builder);
            builder.ToTable(nameof(Comment));

            builder.Property(p => p.Name).IsRequired(true).HasMaxLength(255).HasColumnOrder(ColumnOrder);
            builder.Property(p => p.SurName).IsRequired(true).HasMaxLength(255).HasColumnOrder(ColumnOrder);
            builder.Property(p => p.Email).IsRequired(false).HasMaxLength(255).HasColumnOrder(ColumnOrder);
            builder.Property(p => p.Phone).IsRequired(false).HasMaxLength(255).HasColumnOrder(ColumnOrder);

            builder.Property(p => p.Title).IsRequired(false).HasMaxLength(255).HasColumnOrder(ColumnOrder);
            builder.Property(p => p.CommentText).IsRequired(true).HasMaxLength(500).HasColumnOrder(ColumnOrder);
            builder.Property(p => p.Video).IsRequired(false).HasMaxLength(100).HasColumnOrder(ColumnOrder);
            builder.Property(p => p.Rating).IsRequired().HasDefaultValue(5).HasColumnOrder(ColumnOrder);
            
            //builder.Property(p => p.VillaId).IsRequired(false).HasColumnOrder(ColumnOrder);

            builder
                .HasOne(p => p.Villa)
                .WithMany(c => c.Comments)
                .HasForeignKey(p => p.VillaId)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired(false);

            builder
                .HasOne(p => p.Hotel)
                .WithMany(c => c.Comments)
                .HasForeignKey(p => p.HotelId)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired(false);
        }
    }
}

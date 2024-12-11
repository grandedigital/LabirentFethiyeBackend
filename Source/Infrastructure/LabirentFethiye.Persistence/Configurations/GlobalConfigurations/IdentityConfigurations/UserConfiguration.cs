using LabirentFethiye.Common.Enums;
using LabirentFethiye.Domain.Entities.GlobalEntities.IdentityEntities;
using LabirentFethiye.Domain.Entities.ProjectEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace LabirentFethiye.Persistence.Configurations.GlobalConfigurations.IdentityConfigurations
{
    public class UserConfiguration : IEntityTypeConfiguration<AppUser>
    {
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
            builder.HasKey(u => u.Id);

            builder.HasIndex(u => u.NormalizedUserName).HasName("UserNameIndex").IsUnique();
            builder.HasIndex(u => u.NormalizedEmail).HasName("EmailIndex");

            builder.ToTable("IdentityUsers");

            builder.Property(u => u.ConcurrencyStamp).IsConcurrencyToken();

            builder.Property(u => u.UserName).HasMaxLength(50);
            builder.Property(u => u.NormalizedUserName).HasMaxLength(50);
            builder.Property(u => u.Email).HasMaxLength(100);
            builder.Property(u => u.NormalizedEmail).HasMaxLength(100);
            builder.Property(u => u.Language).HasMaxLength(20);
            builder.Property(p => p.GeneralStatusType).IsRequired().HasDefaultValue(GeneralStatusType.Active);

            //builder.HasData(UserSeedData.UserDatas());


            builder.HasMany<AppUserClaim>().WithOne().HasForeignKey(uc => uc.UserId).IsRequired();
            builder.HasMany<AppUserLogin>().WithOne().HasForeignKey(ul => ul.UserId).IsRequired();
            builder.HasMany<AppUserToken>().WithOne().HasForeignKey(ut => ut.UserId).IsRequired();
            builder.HasMany<AppUserRole>().WithOne().HasForeignKey(ur => ur.UserId).IsRequired();

            //builder.HasOne(c => c.Company).WithMany(u => u.Users).HasForeignKey(c => c.CompanyId).OnDelete(DeleteBehavior.Restrict);


            //     builder.Entity<IdentityUser>().HasData(
            //    new Product { Id = 1, Name = "Product1", Price = 10.0m },
            //    new Product { Id = 2, Name = "Product2", Price = 20.0m },
            //    new Product { Id = 3, Name = "Product3", Price = 30.0m }
            //);

            
        }
    }
}

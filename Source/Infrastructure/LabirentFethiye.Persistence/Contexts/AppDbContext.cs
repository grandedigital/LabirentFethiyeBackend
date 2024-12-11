using LabirentFethiye.Domain.Entities.GlobalEntities;
using LabirentFethiye.Domain.Entities.GlobalEntities.IdentityEntities;
using LabirentFethiye.Domain.Entities.ProjectEntities;
using LabirentFethiye.Domain.Entities.WebSiteEntities;
using LabirentFethiye.Persistence.Configurations.GlobalConfigurations;
using LabirentFethiye.Persistence.Configurations.GlobalConfigurations.IdentityConfigurations;
using LabirentFethiye.Persistence.Configurations.ProjectConfigurations;
using LabirentFethiye.Persistence.Configurations.WebSiteEntityConfiguration;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LabirentFethiye.Persistence.Contexts
{
    public class AppDbContext : IdentityDbContext<AppUser, AppRole, Guid, AppUserClaim, AppUserRole, AppUserLogin, AppRoleClaim, AppUserToken>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            this.ChangeTracker.LazyLoadingEnabled = false;
        }

        public DbSet<AppUser> Users { get; set; }
        public DbSet<AppRole> Roles { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<CategoryDetail> CategoryDetails { get; set; }
        public DbSet<DistanceRuler> DistanceRulers { get; set; }
        public DbSet<DistanceRulerDetail> DistanceRulerDetails { get; set; }
        public DbSet<Feature> Features { get; set; }
        public DbSet<FeatureDetail> FeatureDetails { get; set; }
        public DbSet<Photo> Photos { get; set; }
        public DbSet<PriceDate> PriceDates { get; set; }
        public DbSet<PriceTable> PriceTables { get; set; }
        public DbSet<PriceTableDetail> PriceTableDetails { get; set; }
        public DbSet<Villa> Villas { get; set; }
        public DbSet<VillaCategory> VillaCategories { get; set; }
        public DbSet<VillaDetail> VillaDetails { get; set; }
        public DbSet<VillaFeature> VillaFeatures { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Hotel> Hotels { get; set; }
        public DbSet<HotelDetail> HotelDetails { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<RoomDetail> RoomDetails { get; set; }
        public DbSet<Town> Towns { get; set; }
        public DbSet<District> Districts { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<PaymentType> PaymentTypes { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<ReservationInfo> ReservationInfos { get; set; }
        public DbSet<ReservationItem> ReservationItems { get; set; }


        public DbSet<Menu> Menus { get; set; }
        public DbSet<MenuDetail> MenuDetails { get; set; }
        public DbSet<WebPage> WebPages { get; set; }
        public DbSet<WebPageDetail> WebPageDetails { get; set; }
        public DbSet<WebPhoto> WebPhotos { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

            // Configurations
            #region GlobalConfigurations
            builder.ApplyConfiguration(new CityConfiguration());
            builder.ApplyConfiguration(new DistrictConfiguration());
            builder.ApplyConfiguration(new TownConfiguration());
            #region IdentityConfigurations
            builder.ApplyConfiguration(new RoleClaimConfiguration());
            builder.ApplyConfiguration(new RoleConfiguration());
            builder.ApplyConfiguration(new UserClaimConfiguration());
            builder.ApplyConfiguration(new UserConfiguration());
            builder.ApplyConfiguration(new UserLoginConfiguration());
            builder.ApplyConfiguration(new UserRoleConfiguration());
            builder.ApplyConfiguration(new UserTokenConfiguration());
            #endregion
            #endregion
            #region ProjectConfigurations
            builder.ApplyConfiguration(new CategoryConfiguration());
            builder.ApplyConfiguration(new CategoryDetailConfiguration());

            builder.ApplyConfiguration(new DistanceRulerConfiguration());
            builder.ApplyConfiguration(new DistanceRulerDetailConfiguration());
            builder.ApplyConfiguration(new PhotoConfiguration());
            builder.ApplyConfiguration(new PriceDateConfiguration());
            builder.ApplyConfiguration(new PriceTableConfiguration());
            builder.ApplyConfiguration(new PriceTableDetailConfiguration());
            builder.ApplyConfiguration(new CommentConfiguration());

            builder.ApplyConfiguration(new FeatureConfiguration());

            builder.ApplyConfiguration(new VillaConfiguration());
            builder.ApplyConfiguration(new VillaDetailConfiguration());
            builder.ApplyConfiguration(new VillaFeatureConfiguration());
            builder.ApplyConfiguration(new VillaCategoryConfiguration());

            builder.ApplyConfiguration(new HotelConfiguration());
            builder.ApplyConfiguration(new HotelDetailConfiguration());
            builder.ApplyConfiguration(new RoomConfiguration());
            builder.ApplyConfiguration(new RoomDetailConfiguration());

            builder.ApplyConfiguration(new PaymentTypeConfiguration());
            builder.ApplyConfiguration(new PaymentConfiguration());
            builder.ApplyConfiguration(new PaymentConfiguration());

            builder.ApplyConfiguration(new ReservationConfiguration());
            builder.ApplyConfiguration(new ReservationInfoConfiguration());
            builder.ApplyConfiguration(new ReservationItemConfiguration());
            #endregion
            #region WebSiteConfigurations
            builder.ApplyConfiguration(new MenuConfiguration());
            builder.ApplyConfiguration(new MenuDetailConfiguration());
            builder.ApplyConfiguration(new WebPhotoConfiguration());
            builder.ApplyConfiguration(new WebPageConfiguration());
            builder.ApplyConfiguration(new WebPageDetailConfiguration());
            #endregion
        }

    }
}

//using Microsoft.AspNetCore.Identity;
//using Microsoft.Extensions.Configuration;
//using Microsoft.Extensions.DependencyInjection;
//using LabirentFethiye.Application.Abstract.Services;
//using LabirentFethiye.Domain.Entities.IdentityEntities;
//using LabirentFethiye.Persistence.Concrete;
//using LabirentFethiye.Persistence.Contexts;
using LabirentFethiye.Application.Abstracts.GlobalInterfaces;
using LabirentFethiye.Application.Abstracts.ProjectInterfaces;
using LabirentFethiye.Application.Abstracts.WebSiteInterfaces;
using LabirentFethiye.Persistence.Concrete.GlobalConcretes;
using LabirentFethiye.Persistence.Concrete.ProjectConcretes;
using LabirentFethiye.Persistence.Concrete.WebsiteConcretes;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LabirentFethiye.Persistence
{
    public static class ServiceRegistration
    {
        public static IConfiguration _config;

        public static void Initialize(IConfiguration configuration)
        {
            _config = configuration;
        }

        public static void AddPersistenceServices(this IServiceCollection services)
        {

            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IRoleService, RoleService>();

            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<ICommentService, CommentService>();
            services.AddScoped<IDistanceRulerService, DistanceRulerService>();
            services.AddScoped<IPaymentService, PaymentService>();
            services.AddScoped<IPaymentTypeService, PaymentTypeService>();
            services.AddScoped<IPhotoService, PhotoService>();
            services.AddScoped<IPriceTableService, PriceTableService>();
            services.AddScoped<IRoomService, RoomService>();
            services.AddScoped<ISummaryService, SummaryService>();
            services.AddScoped<ITownService, TownService>();
            services.AddScoped<IVillaService, VillaService>();
            services.AddScoped<IHotelService, HotelService>();
            services.AddScoped<IPriceDateService, PriceDateService>();
            services.AddScoped<IReservationService, ReservationService>();
            services.AddScoped<IReservationInfoService, ReservationInfoService>();
            services.AddScoped<IMenuService, MenuService>();
            services.AddScoped<IWebPageService, WebPageService>();

            services.AddScoped<IClientService, ClientService>(); 

            //    //services.AddIdentity<User, Role>(options =>
            //    //{
            //    //    options.Password.RequiredLength = 6;
            //    //    options.Password.RequireNonAlphanumeric = true;
            //    //    options.Password.RequireUppercase = true;
            //    //    options.Password.RequireLowercase = true;
            //    //    options.Password.RequireDigit = false;

            //    //    options.User.RequireUniqueEmail = true;
            //    //    options.User.AllowedUserNameCharacters = "abcçdefghiıjklmnoöpqrsştuüvwxyzABCÇDEFGHIİJKLMNOÖPQRSŞTUÜVWXYZ0123456789-._@+";

            //    //    options.SignIn.RequireConfirmedEmail = true;

            //    //})
            //    //    .AddEntityFrameworkStores<AppDbContext>()
            //    //    //.AddPasswordValidator<PasswordValidation>()
            //    //    //.AddUserValidator<UserValidation>()
            //    //    //.AddErrorDescriber<CustomIdentityErrorDescriber>()
            //    //    .AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();




            //    //services.AddHttpContextAccessor();

            //    //services.AddScoped<IUserService, UserService>();
            //    //services.AddScoped<IRoleService, RoleService>();
            //    //services.AddScoped<ICompanyService, CompanyService>();
            //    //services.AddScoped<ICategoryService, CategoryService>();
            //    //services.AddScoped<IVillaService, VillaService>();
            //    //services.AddScoped<IHotelService, HotelService>();
            //    //services.AddScoped<IRoomService, RoomService>();
            //    //services.AddScoped<IDistanceRulerService, DistanceRulerService>();
            //    //services.AddScoped<IPriceTableService, PriceTableService>();
            //    //services.AddScoped<IPriceDateService, PriceDateService>();
            //    //services.AddScoped<IReservationService, ReservationService>();
            //    //services.AddScoped<IReservationInfoService, ReservationInfoService>();
            //    //services.AddScoped<IAuthService, AuthService>();
            //    //services.AddScoped<IPhotoService, PhotoService>();
            //    //services.AddScoped<ITownService, TownService>();
            //    //services.AddScoped<IPaymentTypeService, PaymentTypeService>();
            //    //services.AddScoped<IPaymentService, PaymentService>();
            //    //services.AddScoped<ICommentService, CommentService>();

            //    //services.AddScoped<IMenuService, MenuService>();
            //    //services.AddScoped<IWebPageService, WebPageService>();

            //    //services.AddScoped<ISummaryService, SummaryService>();



            //    //services.AddScoped<IClientService, ClientService>();

        }
    }
}

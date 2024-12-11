using FluentValidation.AspNetCore;
using LabirentFethiye.Application;
using LabirentFethiye.Common.Requests;
using LabirentFethiye.Domain.Entities.GlobalEntities.IdentityEntities;
using LabirentFethiye.Infastructure;
using LabirentFethiye.Persistence;
using LabirentFethiye.Persistence.Contexts;
using LabirentFethiye.Persistence.SeedDatas;
using LabirentFethiye.Persistence.Validations.ProjectValidations.CategoryValidations.CaegoryRequestValidations;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using System.Reflection;
using System.Security.Claims;
using System.Text;
var builder = WebApplication.CreateBuilder(args);

IConfiguration configuration = builder.Configuration;


#region DbContext
builder.Services.AddDbContext<AppDbContext>(x =>
{

    x.UseSqlServer(builder.Configuration.GetConnectionString("SqlConStr"), option =>
    {
        option.MigrationsAssembly(Assembly.GetAssembly(typeof(AppDbContext)).GetName().Name);
    });
});
#endregion

#region Registrations
builder.Services.AddHttpContextAccessor();
builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices();
builder.Services.AddPersistenceServices();
//builder.Services.AddInfrastructureServices();
#endregion

#region Authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer("Admin", options =>
    {
        options.SaveToken = true;
        options.RequireHttpsMetadata = false;
        options.TokenValidationParameters = new()
        {
            ValidateAudience = true, // Oluşturulacak token değerini kimlerin/hangi originlerin/sitelerin kullanıcı belirlediğimiz değerdir. -> www.client.com
            ValidateIssuer = true, // Oluşturulacak token değerini kimin dağıttını ifade edeceğimiz alandır. -> www.myapi.com
            ValidateLifetime = true, // Oluşturulan token değerinin süresini kontrol edecek olan doğrulamadır.
            ValidateIssuerSigningKey = true, // Üretilecek token değerinin uygulamamıza ait bir değer olduğunu ifade eden suciry key verisinin doğrulanmasıdır.

            ValidAudience = configuration["Jwt:Audience"],
            ValidIssuer = configuration["Jwt:Issuer"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:SecurityKey"])),
            LifetimeValidator = (notBefore, expires, securityToken, validationParameters) => expires != null ? expires > DateTime.UtcNow : false,
            NameClaimType = ClaimTypes.Name // JWT üzerinde Name claimne karşılık gelen değeri User.Identity.Name propertysinden elde edebiliriz.
        };
    });
#endregion

#region Cors
//builder.Services.AddCors(options =>
//{
//    options.AddPolicy("prodCorsPolicy", builder =>
//    {
//        builder.SetIsOriginAllowed(origin => new Uri(origin).Host == "labirentfethiye");
//    });
//});
//builder.Services.AddCors(options => options.AddDefaultPolicy(policy => policy.WithOrigins("https://localhost:3000", "http://localhost:3000", "https://*.testgrande.com", "http://*.testgrande.com").AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin()));


//builder.Services.AddCors(options => options.AddPolicy("devCorsPolicy", policy => policy.WithExposedHeaders().AllowAnyOrigin().AllowAnyMethod().AllowAnyOrigin() ));

//builder.Services.AddCors(options =>
//{
//    options.AddPolicy("devCorsPolicy", builder =>
//    {
//        builder.SetIsOriginAllowed(origin => new Uri(origin).Host == "localhost");
//    });
//});

//builder.Services.AddCors(options =>
//{
//    options.AddPolicy("prodCorsPolicy", builder =>
//    {
//        builder.SetIsOriginAllowed(origin => new Uri(origin).Host == "labirentfethiye");
//    });
//});

builder.Services.AddCors(options => options.AddDefaultPolicy(builder => builder.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin()));


#endregion





builder.Services.AddIdentity<AppUser, AppRole>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddControllers(); //.AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<CategoryCreateRequestDtoValidator>());





builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseCors(builder.Configuration["Cors:PolicyName"]);
app.UseCors();

app.UseStaticFiles(new StaticFileOptions()
{
    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), @"Uploads")),
    RequestPath = new PathString("/Uploads")
});

app.UseHttpsRedirection();


app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

DatabaseInitilaizer.Seed(app); // Seed verilerini ekleme

app.Run();

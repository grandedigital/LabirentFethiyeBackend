using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LabirentFethiye.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class mig_0_Initital : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "0x0"),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    UpdatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "0x0"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    GeneralStatusType = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    Icon = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: true),
                    Line = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    MetaTitle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MetaDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Slug = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "City",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "0x0"),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    UpdatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "0x0"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    GeneralStatusType = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    Name = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: false),
                    CityNumber = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_City", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "IdentityRoles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IdentityRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "IdentityUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SurName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Language = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    RefreshToken = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RefreshTokenEndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    GeneralStatusType = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    UserName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IdentityUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PaymentType",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "0x0"),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    UpdatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "0x0"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    GeneralStatusType = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(400)", maxLength: 400, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CategoryDetail",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "0x0"),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    UpdatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "0x0"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    GeneralStatusType = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    LanguageCode = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    DescriptionShort = table.Column<string>(type: "nvarchar(400)", maxLength: 400, nullable: true),
                    DescriptionLong = table.Column<string>(type: "nvarchar(max)", maxLength: 5000, nullable: true),
                    CategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryDetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CategoryDetail_Category_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Category",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "District",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "0x0"),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    UpdatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "0x0"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    GeneralStatusType = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    Name = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: false),
                    CityId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_District", x => x.Id);
                    table.ForeignKey(
                        name: "FK_District_City_CityId",
                        column: x => x.CityId,
                        principalTable: "City",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "IdentityRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IdentityRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IdentityRoleClaims_IdentityRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "IdentityRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IdentityUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IdentityUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IdentityUserClaims_IdentityUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "IdentityUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IdentityUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IdentityUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_IdentityUserLogins_IdentityUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "IdentityUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IdentityUserRoles",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IdentityUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_IdentityUserRoles_IdentityRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "IdentityRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_IdentityUserRoles_IdentityUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "IdentityUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IdentityUserTokens",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IdentityUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_IdentityUserTokens_IdentityUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "IdentityUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Town",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "0x0"),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    UpdatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "0x0"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    GeneralStatusType = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    Name = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: false),
                    DistrictId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Town", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Town_District_DistrictId",
                        column: x => x.DistrictId,
                        principalTable: "District",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Hotel",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "0x0"),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    UpdatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "0x0"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    GeneralStatusType = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    Room = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    Person = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    Bath = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    GoogleMap = table.Column<string>(type: "nvarchar(350)", maxLength: 350, nullable: true),
                    WaterMaterNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ElectricityMeterNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    InternetMeterNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    WifiPassword = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    PriceType = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    Line = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    TownId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MetaTitle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MetaDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Slug = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hotel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Hotel_Town_TownId",
                        column: x => x.TownId,
                        principalTable: "Town",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Villa",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "0x0"),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    UpdatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "0x0"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    GeneralStatusType = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    Room = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    Bath = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    Person = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    GoogleMap = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    OnlineReservation = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    IsRent = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    IsSale = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    WaterMaterNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ElectricityMeterNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    InternetMeterNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    WifiPassword = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    VillaNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    VillaOwnerName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    VillaOwnerPhone = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    PriceType = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    Line = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    MinimumReservationNight = table.Column<int>(type: "int", nullable: false, defaultValue: 5),
                    TownId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MetaTitle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MetaDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Slug = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Villa", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Villa_Town_TownId",
                        column: x => x.TownId,
                        principalTable: "Town",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "HotelDetail",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "0x0"),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    UpdatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "0x0"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    GeneralStatusType = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    LanguageCode = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DescriptionShort = table.Column<string>(type: "nvarchar(400)", maxLength: 400, nullable: true),
                    DescriptionLong = table.Column<string>(type: "nvarchar(max)", maxLength: 5000, nullable: true),
                    FeatureTextBlue = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    FeatureTextRed = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    FeatureTextWhite = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    HotelId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HotelDetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HotelDetail_Hotel_HotelId",
                        column: x => x.HotelId,
                        principalTable: "Hotel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Room",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "0x0"),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    UpdatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "0x0"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    GeneralStatusType = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    Rooms = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    Person = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    Bath = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    WaterMaterNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ElectricityMeterNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    InternetMeterNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    WifiPassword = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Line = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    PriceType = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    OnlineReservation = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    HotelId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MetaTitle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MetaDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Slug = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Room", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Room_Hotel_HotelId",
                        column: x => x.HotelId,
                        principalTable: "Hotel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Comment",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "0x0"),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    UpdatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "0x0"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    GeneralStatusType = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    SurName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Title = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    CommentText = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Video = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Rating = table.Column<decimal>(type: "decimal(18,2)", nullable: false, defaultValue: 5m),
                    VillaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    HotelId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comment_Hotel_HotelId",
                        column: x => x.HotelId,
                        principalTable: "Hotel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Comment_Villa_VillaId",
                        column: x => x.VillaId,
                        principalTable: "Villa",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DistanceRuler",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "0x0"),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    UpdatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "0x0"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    GeneralStatusType = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    Icon = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: true),
                    Line = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    VillaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    HotelId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DistanceRuler", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DistanceRuler_Hotel_HotelId",
                        column: x => x.HotelId,
                        principalTable: "Hotel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DistanceRuler_Villa_VillaId",
                        column: x => x.VillaId,
                        principalTable: "Villa",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "VillaCategory",
                columns: table => new
                {
                    VillaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VillaCategory", x => new { x.VillaId, x.CategoryId });
                    table.ForeignKey(
                        name: "FK_VillaCategory_Category_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Category",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VillaCategory_Villa_VillaId",
                        column: x => x.VillaId,
                        principalTable: "Villa",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VillaDetail",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "0x0"),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    UpdatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "0x0"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    GeneralStatusType = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    LanguageCode = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DescriptionShort = table.Column<string>(type: "nvarchar(400)", maxLength: 400, nullable: true),
                    DescriptionLong = table.Column<string>(type: "nvarchar(max)", maxLength: 5000, nullable: true),
                    FeatureTextBlue = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    FeatureTextRed = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    FeatureTextWhite = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    VillaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VillaDetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VillaDetail_Villa_VillaId",
                        column: x => x.VillaId,
                        principalTable: "Villa",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Feature",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "0x0"),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    UpdatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "0x0"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    GeneralStatusType = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    Icon = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: true),
                    Line = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    ParentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    HotelId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    RoomId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Feature", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Feature_Feature_ParentId",
                        column: x => x.ParentId,
                        principalTable: "Feature",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Feature_Hotel_HotelId",
                        column: x => x.HotelId,
                        principalTable: "Hotel",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Feature_Room_RoomId",
                        column: x => x.RoomId,
                        principalTable: "Room",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Payment",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "0x0"),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    UpdatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "0x0"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    GeneralStatusType = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(400)", maxLength: 400, nullable: true),
                    InOrOut = table.Column<bool>(type: "bit", nullable: false),
                    PriceType = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    PaymentTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    VillaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    HotelId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoomId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Payment_Hotel_HotelId",
                        column: x => x.HotelId,
                        principalTable: "Hotel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Payment_PaymentType_PaymentTypeId",
                        column: x => x.PaymentTypeId,
                        principalTable: "PaymentType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Payment_Room_RoomId",
                        column: x => x.RoomId,
                        principalTable: "Room",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Payment_Villa_VillaId",
                        column: x => x.VillaId,
                        principalTable: "Villa",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Photo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "0x0"),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    UpdatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "0x0"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    GeneralStatusType = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ImgAlt = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ImgTitle = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Image = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    VideoLink = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    Line = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    VillaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    HotelId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoomId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Photo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Photo_Hotel_HotelId",
                        column: x => x.HotelId,
                        principalTable: "Hotel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Photo_Room_RoomId",
                        column: x => x.RoomId,
                        principalTable: "Room",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Photo_Villa_VillaId",
                        column: x => x.VillaId,
                        principalTable: "Villa",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PriceDate",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "0x0"),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    UpdatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "0x0"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    GeneralStatusType = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    VillaId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    RoomId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PriceDate", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PriceDate_Room_RoomId",
                        column: x => x.RoomId,
                        principalTable: "Room",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PriceDate_Villa_VillaId",
                        column: x => x.VillaId,
                        principalTable: "Villa",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PriceTable",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "0x0"),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    UpdatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "0x0"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    GeneralStatusType = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    Icon = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: true),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Line = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    VillaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoomId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PriceTable", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PriceTable_Room_RoomId",
                        column: x => x.RoomId,
                        principalTable: "Room",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PriceTable_Villa_VillaId",
                        column: x => x.VillaId,
                        principalTable: "Villa",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Reservation",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "0x0"),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    UpdatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "0x0"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    GeneralStatusType = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    ReservationNumber = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: false),
                    ReservationStatusType = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    ReservationChannalType = table.Column<int>(type: "int", nullable: false, defaultValue: 5),
                    Note = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CheckIn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CheckOut = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDepositPrice = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    IsCleaningPrice = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    Amount = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    Discount = table.Column<decimal>(type: "decimal(10,2)", nullable: false, defaultValue: 0m),
                    ExtraPrice = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    Total = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    PriceType = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    VillaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoomId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reservation_Room_RoomId",
                        column: x => x.RoomId,
                        principalTable: "Room",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Reservation_Villa_VillaId",
                        column: x => x.VillaId,
                        principalTable: "Villa",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RoomDetail",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "0x0"),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    UpdatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "0x0"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    GeneralStatusType = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    LanguageCode = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DescriptionShort = table.Column<string>(type: "nvarchar(400)", maxLength: 400, nullable: true),
                    DescriptionLong = table.Column<string>(type: "nvarchar(max)", maxLength: 5000, nullable: true),
                    FeatureTextBlue = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    FeatureTextRed = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    FeatureTextWhite = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    RoomId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoomDetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RoomDetail_Room_RoomId",
                        column: x => x.RoomId,
                        principalTable: "Room",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DistanceRulerDetail",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "0x0"),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    UpdatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "0x0"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    GeneralStatusType = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    LanguageCode = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Value = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    DistanceRulerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DistanceRulerDetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DistanceRulerDetail_DistanceRuler_DistanceRulerId",
                        column: x => x.DistanceRulerId,
                        principalTable: "DistanceRuler",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FeatureDetails",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LanguageCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FeatureId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    GeneralStatusType = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FeatureDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FeatureDetails_Feature_FeatureId",
                        column: x => x.FeatureId,
                        principalTable: "Feature",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VillaFeature",
                columns: table => new
                {
                    VillaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FeatureId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VillaFeature", x => new { x.VillaId, x.FeatureId });
                    table.ForeignKey(
                        name: "FK_VillaFeature_Feature_FeatureId",
                        column: x => x.FeatureId,
                        principalTable: "Feature",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VillaFeature_Villa_VillaId",
                        column: x => x.VillaId,
                        principalTable: "Villa",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PriceTableDetail",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "0x0"),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    UpdatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "0x0"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    GeneralStatusType = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    LanguageCode = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    PriceTableId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PriceTableDetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PriceTableDetail_PriceTable_PriceTableId",
                        column: x => x.PriceTableId,
                        principalTable: "PriceTable",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ReservationInfo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "0x0"),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    UpdatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "0x0"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    GeneralStatusType = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Surname = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: true),
                    Owner = table.Column<bool>(type: "bit", nullable: false),
                    PeopleType = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    IdNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReservationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReservationInfo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReservationInfo_Reservation_ReservationId",
                        column: x => x.ReservationId,
                        principalTable: "Reservation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ReservationItem",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "0x0"),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    UpdatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "0x0"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    GeneralStatusType = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    Day = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    ReservationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReservationItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReservationItem_Reservation_ReservationId",
                        column: x => x.ReservationId,
                        principalTable: "Reservation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CategoryDetail_CategoryId",
                table: "CategoryDetail",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Comment_HotelId",
                table: "Comment",
                column: "HotelId");

            migrationBuilder.CreateIndex(
                name: "IX_Comment_VillaId",
                table: "Comment",
                column: "VillaId");

            migrationBuilder.CreateIndex(
                name: "IX_DistanceRuler_HotelId",
                table: "DistanceRuler",
                column: "HotelId");

            migrationBuilder.CreateIndex(
                name: "IX_DistanceRuler_VillaId",
                table: "DistanceRuler",
                column: "VillaId");

            migrationBuilder.CreateIndex(
                name: "IX_DistanceRulerDetail_DistanceRulerId",
                table: "DistanceRulerDetail",
                column: "DistanceRulerId");

            migrationBuilder.CreateIndex(
                name: "IX_District_CityId",
                table: "District",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_Feature_HotelId",
                table: "Feature",
                column: "HotelId");

            migrationBuilder.CreateIndex(
                name: "IX_Feature_ParentId",
                table: "Feature",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_Feature_RoomId",
                table: "Feature",
                column: "RoomId");

            migrationBuilder.CreateIndex(
                name: "IX_FeatureDetails_FeatureId",
                table: "FeatureDetails",
                column: "FeatureId");

            migrationBuilder.CreateIndex(
                name: "IX_Hotel_TownId",
                table: "Hotel",
                column: "TownId");

            migrationBuilder.CreateIndex(
                name: "IX_HotelDetail_HotelId",
                table: "HotelDetail",
                column: "HotelId");

            migrationBuilder.CreateIndex(
                name: "IX_IdentityRoleClaims_RoleId",
                table: "IdentityRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "IdentityRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_IdentityUserClaims_UserId",
                table: "IdentityUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_IdentityUserLogins_UserId",
                table: "IdentityUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_IdentityUserRoles_RoleId",
                table: "IdentityUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "IdentityUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "IdentityUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Payment_HotelId",
                table: "Payment",
                column: "HotelId");

            migrationBuilder.CreateIndex(
                name: "IX_Payment_PaymentTypeId",
                table: "Payment",
                column: "PaymentTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Payment_RoomId",
                table: "Payment",
                column: "RoomId");

            migrationBuilder.CreateIndex(
                name: "IX_Payment_VillaId",
                table: "Payment",
                column: "VillaId");

            migrationBuilder.CreateIndex(
                name: "IX_Photo_HotelId",
                table: "Photo",
                column: "HotelId");

            migrationBuilder.CreateIndex(
                name: "IX_Photo_RoomId",
                table: "Photo",
                column: "RoomId");

            migrationBuilder.CreateIndex(
                name: "IX_Photo_VillaId",
                table: "Photo",
                column: "VillaId");

            migrationBuilder.CreateIndex(
                name: "IX_PriceDate_RoomId",
                table: "PriceDate",
                column: "RoomId");

            migrationBuilder.CreateIndex(
                name: "IX_PriceDate_VillaId",
                table: "PriceDate",
                column: "VillaId");

            migrationBuilder.CreateIndex(
                name: "IX_PriceTable_RoomId",
                table: "PriceTable",
                column: "RoomId");

            migrationBuilder.CreateIndex(
                name: "IX_PriceTable_VillaId",
                table: "PriceTable",
                column: "VillaId");

            migrationBuilder.CreateIndex(
                name: "IX_PriceTableDetail_PriceTableId",
                table: "PriceTableDetail",
                column: "PriceTableId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservation_RoomId",
                table: "Reservation",
                column: "RoomId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservation_VillaId",
                table: "Reservation",
                column: "VillaId");

            migrationBuilder.CreateIndex(
                name: "IX_ReservationInfo_ReservationId",
                table: "ReservationInfo",
                column: "ReservationId");

            migrationBuilder.CreateIndex(
                name: "IX_ReservationItem_ReservationId",
                table: "ReservationItem",
                column: "ReservationId");

            migrationBuilder.CreateIndex(
                name: "IX_Room_HotelId",
                table: "Room",
                column: "HotelId");

            migrationBuilder.CreateIndex(
                name: "IX_RoomDetail_RoomId",
                table: "RoomDetail",
                column: "RoomId");

            migrationBuilder.CreateIndex(
                name: "IX_Town_DistrictId",
                table: "Town",
                column: "DistrictId");

            migrationBuilder.CreateIndex(
                name: "IX_Villa_TownId",
                table: "Villa",
                column: "TownId");

            migrationBuilder.CreateIndex(
                name: "IX_Villa_VillaNumber",
                table: "Villa",
                column: "VillaNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_VillaCategory_CategoryId",
                table: "VillaCategory",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_VillaDetail_VillaId",
                table: "VillaDetail",
                column: "VillaId");

            migrationBuilder.CreateIndex(
                name: "IX_VillaFeature_FeatureId",
                table: "VillaFeature",
                column: "FeatureId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CategoryDetail");

            migrationBuilder.DropTable(
                name: "Comment");

            migrationBuilder.DropTable(
                name: "DistanceRulerDetail");

            migrationBuilder.DropTable(
                name: "FeatureDetails");

            migrationBuilder.DropTable(
                name: "HotelDetail");

            migrationBuilder.DropTable(
                name: "IdentityRoleClaims");

            migrationBuilder.DropTable(
                name: "IdentityUserClaims");

            migrationBuilder.DropTable(
                name: "IdentityUserLogins");

            migrationBuilder.DropTable(
                name: "IdentityUserRoles");

            migrationBuilder.DropTable(
                name: "IdentityUserTokens");

            migrationBuilder.DropTable(
                name: "Payment");

            migrationBuilder.DropTable(
                name: "Photo");

            migrationBuilder.DropTable(
                name: "PriceDate");

            migrationBuilder.DropTable(
                name: "PriceTableDetail");

            migrationBuilder.DropTable(
                name: "ReservationInfo");

            migrationBuilder.DropTable(
                name: "ReservationItem");

            migrationBuilder.DropTable(
                name: "RoomDetail");

            migrationBuilder.DropTable(
                name: "VillaCategory");

            migrationBuilder.DropTable(
                name: "VillaDetail");

            migrationBuilder.DropTable(
                name: "VillaFeature");

            migrationBuilder.DropTable(
                name: "DistanceRuler");

            migrationBuilder.DropTable(
                name: "IdentityRoles");

            migrationBuilder.DropTable(
                name: "IdentityUsers");

            migrationBuilder.DropTable(
                name: "PaymentType");

            migrationBuilder.DropTable(
                name: "PriceTable");

            migrationBuilder.DropTable(
                name: "Reservation");

            migrationBuilder.DropTable(
                name: "Category");

            migrationBuilder.DropTable(
                name: "Feature");

            migrationBuilder.DropTable(
                name: "Villa");

            migrationBuilder.DropTable(
                name: "Room");

            migrationBuilder.DropTable(
                name: "Hotel");

            migrationBuilder.DropTable(
                name: "Town");

            migrationBuilder.DropTable(
                name: "District");

            migrationBuilder.DropTable(
                name: "City");
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LabirentFethiye.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class mig_2_WebSiteEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Menu",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "0x0"),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    UpdatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "0x0"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    GeneralStatusType = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    MetaDescription = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    MetaTitle = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Slug = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    PageType = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Menu", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MenuDetail",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "0x0"),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    UpdatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "0x0"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    GeneralStatusType = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    LanguageCode = table.Column<string>(type: "nvarchar(max)", nullable: false, defaultValue: "tr"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    MenuId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MenuDetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MenuDetail_Menu_MenuId",
                        column: x => x.MenuId,
                        principalTable: "Menu",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WebPage",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "0x0"),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    UpdatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "0x0"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    GeneralStatusType = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    MetaDescription = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    MetaTitle = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Slug = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    MenuId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WebPage", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WebPage_Menu_MenuId",
                        column: x => x.MenuId,
                        principalTable: "Menu",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WebPageDetail",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "0x0"),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    UpdatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "0x0"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    GeneralStatusType = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    LanguageCode = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    DescriptionShort = table.Column<string>(type: "nvarchar(400)", maxLength: 400, nullable: true),
                    DescriptionLong = table.Column<string>(type: "nvarchar(max)", maxLength: 5000, nullable: true),
                    WebPageId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WebPageDetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WebPageDetail_WebPage_WebPageId",
                        column: x => x.WebPageId,
                        principalTable: "WebPage",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WebPhoto",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "0x0"),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    UpdatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "0x0"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    GeneralStatusType = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ImgAlt = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ImgTitle = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Image = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    VideoLink = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    Line = table.Column<int>(type: "int", nullable: false),
                    WebPageId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WebPhoto", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WebPhoto_WebPage_WebPageId",
                        column: x => x.WebPageId,
                        principalTable: "WebPage",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Menu_Slug",
                table: "Menu",
                column: "Slug");

            migrationBuilder.CreateIndex(
                name: "IX_MenuDetail_MenuId",
                table: "MenuDetail",
                column: "MenuId");

            migrationBuilder.CreateIndex(
                name: "IX_WebPage_MenuId",
                table: "WebPage",
                column: "MenuId");

            migrationBuilder.CreateIndex(
                name: "IX_WebPage_Slug",
                table: "WebPage",
                column: "Slug");

            migrationBuilder.CreateIndex(
                name: "IX_WebPageDetail_WebPageId",
                table: "WebPageDetail",
                column: "WebPageId");

            migrationBuilder.CreateIndex(
                name: "IX_WebPhoto_WebPageId",
                table: "WebPhoto",
                column: "WebPageId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MenuDetail");

            migrationBuilder.DropTable(
                name: "WebPageDetail");

            migrationBuilder.DropTable(
                name: "WebPhoto");

            migrationBuilder.DropTable(
                name: "WebPage");

            migrationBuilder.DropTable(
                name: "Menu");
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LabirentFethiye.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class mig_3_Villa_Personal_Edit_1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Villa_PersonalId",
                table: "Villa");

            migrationBuilder.DropColumn(
                name: "VillaId",
                table: "IdentityUsers");

            migrationBuilder.CreateIndex(
                name: "IX_Villa_PersonalId",
                table: "Villa",
                column: "PersonalId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Villa_PersonalId",
                table: "Villa");

            migrationBuilder.AddColumn<Guid>(
                name: "VillaId",
                table: "IdentityUsers",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Villa_PersonalId",
                table: "Villa",
                column: "PersonalId",
                unique: true,
                filter: "[PersonalId] IS NOT NULL");
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LabirentFethiye.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class mig_3_Villa_Personal : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "PersonalId",
                table: "Villa",
                type: "uniqueidentifier",
                nullable: true);

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

            migrationBuilder.AddForeignKey(
                name: "FK_Villa_IdentityUsers_PersonalId",
                table: "Villa",
                column: "PersonalId",
                principalTable: "IdentityUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Villa_IdentityUsers_PersonalId",
                table: "Villa");

            migrationBuilder.DropIndex(
                name: "IX_Villa_PersonalId",
                table: "Villa");

            migrationBuilder.DropColumn(
                name: "PersonalId",
                table: "Villa");

            migrationBuilder.DropColumn(
                name: "VillaId",
                table: "IdentityUsers");
        }
    }
}

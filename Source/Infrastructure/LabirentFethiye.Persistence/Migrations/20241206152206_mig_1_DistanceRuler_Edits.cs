using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LabirentFethiye.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class mig_1_DistanceRuler_Edits : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Value",
                table: "DistanceRulerDetail");

            migrationBuilder.AddColumn<string>(
                name: "Value",
                table: "DistanceRuler",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "")
                .Annotation("Relational:ColumnOrder", 9);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Value",
                table: "DistanceRuler");

            migrationBuilder.AddColumn<string>(
                name: "Value",
                table: "DistanceRulerDetail",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "")
                .Annotation("Relational:ColumnOrder", 9);
        }
    }
}

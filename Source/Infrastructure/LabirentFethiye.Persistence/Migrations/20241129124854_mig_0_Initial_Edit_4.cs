using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LabirentFethiye.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class mig_0_Initial_Edit_4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DistanceRulerDetail_DistanceRuler_DistanceRulerId",
                table: "DistanceRulerDetail");

            migrationBuilder.DropForeignKey(
                name: "FK_PriceTableDetail_PriceTable_PriceTableId",
                table: "PriceTableDetail");

            migrationBuilder.AddForeignKey(
                name: "FK_DistanceRulerDetail_DistanceRuler_DistanceRulerId",
                table: "DistanceRulerDetail",
                column: "DistanceRulerId",
                principalTable: "DistanceRuler",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PriceTableDetail_PriceTable_PriceTableId",
                table: "PriceTableDetail",
                column: "PriceTableId",
                principalTable: "PriceTable",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DistanceRulerDetail_DistanceRuler_DistanceRulerId",
                table: "DistanceRulerDetail");

            migrationBuilder.DropForeignKey(
                name: "FK_PriceTableDetail_PriceTable_PriceTableId",
                table: "PriceTableDetail");

            migrationBuilder.AddForeignKey(
                name: "FK_DistanceRulerDetail_DistanceRuler_DistanceRulerId",
                table: "DistanceRulerDetail",
                column: "DistanceRulerId",
                principalTable: "DistanceRuler",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PriceTableDetail_PriceTable_PriceTableId",
                table: "PriceTableDetail",
                column: "PriceTableId",
                principalTable: "PriceTable",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

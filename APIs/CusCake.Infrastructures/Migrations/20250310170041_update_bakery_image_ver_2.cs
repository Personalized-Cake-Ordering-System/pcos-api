using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CusCake.Infrastructures.Migrations
{
    /// <inheritdoc />
    public partial class update_bakery_image_ver_2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_storages_available_cakes_AvailableCakeId",
                table: "storages");

            migrationBuilder.DropForeignKey(
                name: "FK_storages_bakeries_BakeryId",
                table: "storages");

            migrationBuilder.AddForeignKey(
                name: "FK_storages_available_cakes_AvailableCakeId",
                table: "storages",
                column: "AvailableCakeId",
                principalTable: "available_cakes",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_storages_bakeries_BakeryId",
                table: "storages",
                column: "BakeryId",
                principalTable: "bakeries",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_storages_available_cakes_AvailableCakeId",
                table: "storages");

            migrationBuilder.DropForeignKey(
                name: "FK_storages_bakeries_BakeryId",
                table: "storages");

            migrationBuilder.AddForeignKey(
                name: "FK_storages_available_cakes_AvailableCakeId",
                table: "storages",
                column: "AvailableCakeId",
                principalTable: "available_cakes",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_storages_bakeries_BakeryId",
                table: "storages",
                column: "BakeryId",
                principalTable: "bakeries",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

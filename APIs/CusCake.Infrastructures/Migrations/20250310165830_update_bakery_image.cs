using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CusCake.Infrastructures.Migrations
{
    /// <inheritdoc />
    public partial class update_bakery_image : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_available_cakes_storages_available_main_image_id",
                table: "available_cakes");

            migrationBuilder.DropForeignKey(
                name: "FK_bakeries_storages_avatar_file_id",
                table: "bakeries");

            migrationBuilder.DropForeignKey(
                name: "FK_bakeries_storages_back_card_file_id",
                table: "bakeries");

            migrationBuilder.DropForeignKey(
                name: "FK_bakeries_storages_font_card_file_id",
                table: "bakeries");

            migrationBuilder.AddForeignKey(
                name: "FK_available_cakes_storages_available_main_image_id",
                table: "available_cakes",
                column: "available_main_image_id",
                principalTable: "storages",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_bakeries_storages_avatar_file_id",
                table: "bakeries",
                column: "avatar_file_id",
                principalTable: "storages",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_bakeries_storages_back_card_file_id",
                table: "bakeries",
                column: "back_card_file_id",
                principalTable: "storages",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_bakeries_storages_font_card_file_id",
                table: "bakeries",
                column: "font_card_file_id",
                principalTable: "storages",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_available_cakes_storages_available_main_image_id",
                table: "available_cakes");

            migrationBuilder.DropForeignKey(
                name: "FK_bakeries_storages_avatar_file_id",
                table: "bakeries");

            migrationBuilder.DropForeignKey(
                name: "FK_bakeries_storages_back_card_file_id",
                table: "bakeries");

            migrationBuilder.DropForeignKey(
                name: "FK_bakeries_storages_font_card_file_id",
                table: "bakeries");

            migrationBuilder.AddForeignKey(
                name: "FK_available_cakes_storages_available_main_image_id",
                table: "available_cakes",
                column: "available_main_image_id",
                principalTable: "storages",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_bakeries_storages_avatar_file_id",
                table: "bakeries",
                column: "avatar_file_id",
                principalTable: "storages",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_bakeries_storages_back_card_file_id",
                table: "bakeries",
                column: "back_card_file_id",
                principalTable: "storages",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_bakeries_storages_font_card_file_id",
                table: "bakeries",
                column: "font_card_file_id",
                principalTable: "storages",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

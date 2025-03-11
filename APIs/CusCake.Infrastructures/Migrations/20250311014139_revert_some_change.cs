using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CusCake.Infrastructures.Migrations
{
    /// <inheritdoc />
    public partial class revert_some_change : Migration
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

            migrationBuilder.DropForeignKey(
                name: "FK_storages_available_cakes_AvailableCakeId",
                table: "storages");

            migrationBuilder.DropForeignKey(
                name: "FK_storages_bakeries_BakeryId",
                table: "storages");

            migrationBuilder.DropIndex(
                name: "IX_storages_AvailableCakeId",
                table: "storages");

            migrationBuilder.DropIndex(
                name: "IX_storages_BakeryId",
                table: "storages");

            migrationBuilder.DropColumn(
                name: "AvailableCakeId",
                table: "storages");

            migrationBuilder.DropColumn(
                name: "BakeryId",
                table: "storages");

            migrationBuilder.AddColumn<string>(
                name: "shop_image_files",
                table: "bakeries",
                type: "json",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "available_cake_image_files",
                table: "available_cakes",
                type: "json",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

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

            migrationBuilder.DropColumn(
                name: "shop_image_files",
                table: "bakeries");

            migrationBuilder.DropColumn(
                name: "available_cake_image_files",
                table: "available_cakes");

            migrationBuilder.AddColumn<Guid>(
                name: "AvailableCakeId",
                table: "storages",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<Guid>(
                name: "BakeryId",
                table: "storages",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.CreateIndex(
                name: "IX_storages_AvailableCakeId",
                table: "storages",
                column: "AvailableCakeId");

            migrationBuilder.CreateIndex(
                name: "IX_storages_BakeryId",
                table: "storages",
                column: "BakeryId");

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
    }
}

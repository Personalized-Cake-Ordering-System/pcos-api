using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CusCake.Infrastructures.Migrations
{
    /// <inheritdoc />
    public partial class update_cake_part : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "is_default",
                table: "cake_parts",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<Guid>(
                name: "part_image_id",
                table: "cake_parts",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci");

            migrationBuilder.AlterColumn<Guid>(
                name: "avatar_file_id",
                table: "bakeries",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci",
                oldClrType: typeof(Guid),
                oldType: "char(36)")
                .OldAnnotation("Relational:Collation", "ascii_general_ci");

            migrationBuilder.AddColumn<Guid>(
                name: "available_main_image_id",
                table: "available_cakes",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.CreateIndex(
                name: "IX_cake_parts_part_image_id",
                table: "cake_parts",
                column: "part_image_id");

            migrationBuilder.CreateIndex(
                name: "IX_bakeries_avatar_file_id",
                table: "bakeries",
                column: "avatar_file_id");

            migrationBuilder.CreateIndex(
                name: "IX_bakeries_back_card_file_id",
                table: "bakeries",
                column: "back_card_file_id");

            migrationBuilder.CreateIndex(
                name: "IX_bakeries_font_card_file_id",
                table: "bakeries",
                column: "font_card_file_id");

            migrationBuilder.CreateIndex(
                name: "IX_available_cakes_available_main_image_id",
                table: "available_cakes",
                column: "available_main_image_id");

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

            migrationBuilder.AddForeignKey(
                name: "FK_cake_parts_storages_part_image_id",
                table: "cake_parts",
                column: "part_image_id",
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

            migrationBuilder.DropForeignKey(
                name: "FK_cake_parts_storages_part_image_id",
                table: "cake_parts");

            migrationBuilder.DropIndex(
                name: "IX_cake_parts_part_image_id",
                table: "cake_parts");

            migrationBuilder.DropIndex(
                name: "IX_bakeries_avatar_file_id",
                table: "bakeries");

            migrationBuilder.DropIndex(
                name: "IX_bakeries_back_card_file_id",
                table: "bakeries");

            migrationBuilder.DropIndex(
                name: "IX_bakeries_font_card_file_id",
                table: "bakeries");

            migrationBuilder.DropIndex(
                name: "IX_available_cakes_available_main_image_id",
                table: "available_cakes");

            migrationBuilder.DropColumn(
                name: "is_default",
                table: "cake_parts");

            migrationBuilder.DropColumn(
                name: "part_image_id",
                table: "cake_parts");

            migrationBuilder.DropColumn(
                name: "available_main_image_id",
                table: "available_cakes");

            migrationBuilder.AlterColumn<Guid>(
                name: "avatar_file_id",
                table: "bakeries",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci",
                oldClrType: typeof(Guid),
                oldType: "char(36)",
                oldNullable: true)
                .OldAnnotation("Relational:Collation", "ascii_general_ci");
        }
    }
}

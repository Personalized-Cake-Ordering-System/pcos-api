using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CusCake.Infrastructures.Migrations
{
    /// <inheritdoc />
    public partial class update_cake_extra : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "extra_number",
                table: "cake_extras");

            migrationBuilder.AddColumn<Guid>(
                name: "BakeryId",
                table: "cake_parts",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<string>(
                name: "part_color",
                table: "cake_parts",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<Guid>(
                name: "BakeryId",
                table: "cake_extras",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<string>(
                name: "extra_color",
                table: "cake_extras",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<Guid>(
                name: "extra_image_id",
                table: "cake_extras",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<bool>(
                name: "is_default",
                table: "cake_extras",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_cake_extras_extra_image_id",
                table: "cake_extras",
                column: "extra_image_id");

            migrationBuilder.AddForeignKey(
                name: "FK_cake_extras_storages_extra_image_id",
                table: "cake_extras",
                column: "extra_image_id",
                principalTable: "storages",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_cake_extras_storages_extra_image_id",
                table: "cake_extras");

            migrationBuilder.DropIndex(
                name: "IX_cake_extras_extra_image_id",
                table: "cake_extras");

            migrationBuilder.DropColumn(
                name: "BakeryId",
                table: "cake_parts");

            migrationBuilder.DropColumn(
                name: "part_color",
                table: "cake_parts");

            migrationBuilder.DropColumn(
                name: "BakeryId",
                table: "cake_extras");

            migrationBuilder.DropColumn(
                name: "extra_color",
                table: "cake_extras");

            migrationBuilder.DropColumn(
                name: "extra_image_id",
                table: "cake_extras");

            migrationBuilder.DropColumn(
                name: "is_default",
                table: "cake_extras");

            migrationBuilder.AddColumn<double>(
                name: "extra_number",
                table: "cake_extras",
                type: "double",
                nullable: true);
        }
    }
}

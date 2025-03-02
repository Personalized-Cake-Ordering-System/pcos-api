using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CusCake.Infrastructures.Migrations
{
    /// <inheritdoc />
    public partial class update_cake_decoration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "BakeryId",
                table: "cake_decorations",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<string>(
                name: "decoration_color",
                table: "cake_decorations",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<Guid>(
                name: "decoration_image_id",
                table: "cake_decorations",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.CreateIndex(
                name: "IX_cake_decorations_decoration_image_id",
                table: "cake_decorations",
                column: "decoration_image_id");

            migrationBuilder.AddForeignKey(
                name: "FK_cake_decorations_storages_decoration_image_id",
                table: "cake_decorations",
                column: "decoration_image_id",
                principalTable: "storages",
                principalColumn: "id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_cake_decorations_storages_decoration_image_id",
                table: "cake_decorations");

            migrationBuilder.DropIndex(
                name: "IX_cake_decorations_decoration_image_id",
                table: "cake_decorations");

            migrationBuilder.DropColumn(
                name: "BakeryId",
                table: "cake_decorations");

            migrationBuilder.DropColumn(
                name: "decoration_color",
                table: "cake_decorations");

            migrationBuilder.DropColumn(
                name: "decoration_image_id",
                table: "cake_decorations");
        }
    }
}

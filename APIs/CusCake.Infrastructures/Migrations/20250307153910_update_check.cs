using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CusCake.Infrastructures.Migrations
{
    /// <inheritdoc />
    public partial class update_check : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "BakeryId",
                table: "cake_parts",
                newName: "bakery_id");

            migrationBuilder.RenameColumn(
                name: "BakeryId",
                table: "cake_messages",
                newName: "bakery_id");

            migrationBuilder.RenameColumn(
                name: "BakeryId",
                table: "cake_extras",
                newName: "bakery_id");

            migrationBuilder.RenameColumn(
                name: "BakeryId",
                table: "cake_decorations",
                newName: "bakery_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "bakery_id",
                table: "cake_parts",
                newName: "BakeryId");

            migrationBuilder.RenameColumn(
                name: "bakery_id",
                table: "cake_messages",
                newName: "BakeryId");

            migrationBuilder.RenameColumn(
                name: "bakery_id",
                table: "cake_extras",
                newName: "BakeryId");

            migrationBuilder.RenameColumn(
                name: "bakery_id",
                table: "cake_decorations",
                newName: "BakeryId");
        }
    }
}

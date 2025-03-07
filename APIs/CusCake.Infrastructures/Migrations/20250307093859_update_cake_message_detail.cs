using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CusCake.Infrastructures.Migrations
{
    /// <inheritdoc />
    public partial class update_cake_message_detail : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "message_type_color",
                table: "cake_message_details");

            migrationBuilder.DropColumn(
                name: "message_type_name",
                table: "cake_message_details");

            migrationBuilder.AddColumn<string>(
                name: "message_type_details",
                table: "cake_message_details",
                type: "json",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<bool>(
                name: "is_default",
                table: "cake_decorations",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "message_type_details",
                table: "cake_message_details");

            migrationBuilder.DropColumn(
                name: "is_default",
                table: "cake_decorations");

            migrationBuilder.AddColumn<string>(
                name: "message_type_color",
                table: "cake_message_details",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "message_type_name",
                table: "cake_message_details",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }
    }
}

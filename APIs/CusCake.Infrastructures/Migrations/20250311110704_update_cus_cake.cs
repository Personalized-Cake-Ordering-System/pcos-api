using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CusCake.Infrastructures.Migrations
{
    /// <inheritdoc />
    public partial class update_cus_cake : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_custom_cakes_message_selection_id",
                table: "custom_cakes");

            migrationBuilder.DropColumn(
                name: "custom_cake_id",
                table: "cake_message_selections");

            migrationBuilder.AddColumn<Guid>(
                name: "image_id",
                table: "cake_message_selections",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<string>(
                name: "text",
                table: "cake_message_selections",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_custom_cakes_message_selection_id",
                table: "custom_cakes",
                column: "message_selection_id");

            migrationBuilder.CreateIndex(
                name: "IX_cake_message_selections_image_id",
                table: "cake_message_selections",
                column: "image_id");

            migrationBuilder.AddForeignKey(
                name: "FK_cake_message_selections_storages_image_id",
                table: "cake_message_selections",
                column: "image_id",
                principalTable: "storages",
                principalColumn: "id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_cake_message_selections_storages_image_id",
                table: "cake_message_selections");

            migrationBuilder.DropIndex(
                name: "IX_custom_cakes_message_selection_id",
                table: "custom_cakes");

            migrationBuilder.DropIndex(
                name: "IX_cake_message_selections_image_id",
                table: "cake_message_selections");

            migrationBuilder.DropColumn(
                name: "image_id",
                table: "cake_message_selections");

            migrationBuilder.DropColumn(
                name: "text",
                table: "cake_message_selections");

            migrationBuilder.AddColumn<Guid>(
                name: "custom_cake_id",
                table: "cake_message_selections",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci");

            migrationBuilder.CreateIndex(
                name: "IX_custom_cakes_message_selection_id",
                table: "custom_cakes",
                column: "message_selection_id",
                unique: true);
        }
    }
}

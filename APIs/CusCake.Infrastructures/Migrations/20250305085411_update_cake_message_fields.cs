using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CusCake.Infrastructures.Migrations
{
    /// <inheritdoc />
    public partial class update_cake_message_fields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_cake_message_details_storages_image_file_id",
                table: "cake_message_details");

            migrationBuilder.DropColumn(
                name: "message_color",
                table: "cake_messages");

            migrationBuilder.RenameColumn(
                name: "type_name",
                table: "cake_message_types",
                newName: "message_type");

            migrationBuilder.RenameColumn(
                name: "type_color",
                table: "cake_message_types",
                newName: "message_color");

            migrationBuilder.RenameColumn(
                name: "image_file_id",
                table: "cake_message_details",
                newName: "message_image_id");

            migrationBuilder.RenameIndex(
                name: "IX_cake_message_details_image_file_id",
                table: "cake_message_details",
                newName: "IX_cake_message_details_message_image_id");

            migrationBuilder.AddColumn<Guid>(
                name: "BakeryId",
                table: "cake_messages",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<string>(
                name: "message_name",
                table: "cake_message_types",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "message_type",
                table: "cake_message_details",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

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

            migrationBuilder.CreateIndex(
                name: "IX_cake_messages_message_image_id",
                table: "cake_messages",
                column: "message_image_id");

            migrationBuilder.AddForeignKey(
                name: "FK_cake_message_details_storages_message_image_id",
                table: "cake_message_details",
                column: "message_image_id",
                principalTable: "storages",
                principalColumn: "id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_cake_messages_storages_message_image_id",
                table: "cake_messages",
                column: "message_image_id",
                principalTable: "storages",
                principalColumn: "id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_cake_message_details_storages_message_image_id",
                table: "cake_message_details");

            migrationBuilder.DropForeignKey(
                name: "FK_cake_messages_storages_message_image_id",
                table: "cake_messages");

            migrationBuilder.DropIndex(
                name: "IX_cake_messages_message_image_id",
                table: "cake_messages");

            migrationBuilder.DropColumn(
                name: "BakeryId",
                table: "cake_messages");

            migrationBuilder.DropColumn(
                name: "message_name",
                table: "cake_message_types");

            migrationBuilder.DropColumn(
                name: "message_type",
                table: "cake_message_details");

            migrationBuilder.DropColumn(
                name: "message_type_color",
                table: "cake_message_details");

            migrationBuilder.DropColumn(
                name: "message_type_name",
                table: "cake_message_details");

            migrationBuilder.RenameColumn(
                name: "message_type",
                table: "cake_message_types",
                newName: "type_name");

            migrationBuilder.RenameColumn(
                name: "message_color",
                table: "cake_message_types",
                newName: "type_color");

            migrationBuilder.RenameColumn(
                name: "message_image_id",
                table: "cake_message_details",
                newName: "image_file_id");

            migrationBuilder.RenameIndex(
                name: "IX_cake_message_details_message_image_id",
                table: "cake_message_details",
                newName: "IX_cake_message_details_image_file_id");

            migrationBuilder.AddColumn<string>(
                name: "message_color",
                table: "cake_messages",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddForeignKey(
                name: "FK_cake_message_details_storages_image_file_id",
                table: "cake_message_details",
                column: "image_file_id",
                principalTable: "storages",
                principalColumn: "id",
                onDelete: ReferentialAction.SetNull);
        }
    }
}

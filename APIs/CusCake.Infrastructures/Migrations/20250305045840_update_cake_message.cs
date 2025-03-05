using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CusCake.Infrastructures.Migrations
{
    /// <inheritdoc />
    public partial class update_cake_message : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_cake_messages_custom_cakes_custom_cake_id",
                table: "cake_messages");

            migrationBuilder.DropIndex(
                name: "IX_cake_messages_custom_cake_id",
                table: "cake_messages");

            migrationBuilder.DropColumn(
                name: "custom_cake_id",
                table: "cake_messages");

            migrationBuilder.DropColumn(
                name: "message",
                table: "cake_messages");

            migrationBuilder.AlterColumn<Guid>(
                name: "message_image_id",
                table: "cake_messages",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci",
                oldClrType: typeof(Guid),
                oldType: "char(36)")
                .OldAnnotation("Relational:Collation", "ascii_general_ci");

            migrationBuilder.AddColumn<string>(
                name: "message_name",
                table: "cake_messages",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<double>(
                name: "message_price",
                table: "cake_messages",
                type: "double",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.CreateTable(
                name: "cake_message_details",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    cake_message_id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    custom_cake_id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    image_file_id = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    message = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    created_at = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    created_by = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    updated_at = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    updated_by = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    is_deleted = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cake_message_details", x => x.id);
                    table.ForeignKey(
                        name: "FK_cake_message_details_cake_messages_cake_message_id",
                        column: x => x.cake_message_id,
                        principalTable: "cake_messages",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_cake_message_details_custom_cakes_custom_cake_id",
                        column: x => x.custom_cake_id,
                        principalTable: "custom_cakes",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_cake_message_details_storages_image_file_id",
                        column: x => x.image_file_id,
                        principalTable: "storages",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "cake_message_types",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    type_name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    type_color = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    cake_message_id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    created_at = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    created_by = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    updated_at = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    updated_by = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    is_deleted = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cake_message_types", x => x.id);
                    table.ForeignKey(
                        name: "FK_cake_message_types_cake_messages_cake_message_id",
                        column: x => x.cake_message_id,
                        principalTable: "cake_messages",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_cake_message_details_cake_message_id",
                table: "cake_message_details",
                column: "cake_message_id");

            migrationBuilder.CreateIndex(
                name: "IX_cake_message_details_custom_cake_id",
                table: "cake_message_details",
                column: "custom_cake_id");

            migrationBuilder.CreateIndex(
                name: "IX_cake_message_details_image_file_id",
                table: "cake_message_details",
                column: "image_file_id");

            migrationBuilder.CreateIndex(
                name: "IX_cake_message_types_cake_message_id",
                table: "cake_message_types",
                column: "cake_message_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "cake_message_details");

            migrationBuilder.DropTable(
                name: "cake_message_types");

            migrationBuilder.DropColumn(
                name: "message_name",
                table: "cake_messages");

            migrationBuilder.DropColumn(
                name: "message_price",
                table: "cake_messages");

            migrationBuilder.AlterColumn<Guid>(
                name: "message_image_id",
                table: "cake_messages",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci",
                oldClrType: typeof(Guid),
                oldType: "char(36)",
                oldNullable: true)
                .OldAnnotation("Relational:Collation", "ascii_general_ci");

            migrationBuilder.AddColumn<Guid>(
                name: "custom_cake_id",
                table: "cake_messages",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<string>(
                name: "message",
                table: "cake_messages",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_cake_messages_custom_cake_id",
                table: "cake_messages",
                column: "custom_cake_id");

            migrationBuilder.AddForeignKey(
                name: "FK_cake_messages_custom_cakes_custom_cake_id",
                table: "cake_messages",
                column: "custom_cake_id",
                principalTable: "custom_cakes",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

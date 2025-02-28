using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CusCake.Infrastructures.Migrations
{
    /// <inheritdoc />
    public partial class update_available_cakes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_bakeries_bakery_name",
                table: "bakeries");

            migrationBuilder.DropIndex(
                name: "IX_bakeries_email",
                table: "bakeries");

            migrationBuilder.DropIndex(
                name: "IX_bakeries_identity_card_number",
                table: "bakeries");

            migrationBuilder.DropIndex(
                name: "IX_bakeries_phone",
                table: "bakeries");

            migrationBuilder.DropIndex(
                name: "IX_bakeries_tax_code",
                table: "bakeries");

            migrationBuilder.DropColumn(
                name: "available_cake_image_id",
                table: "available_cakes");

            migrationBuilder.AlterColumn<string>(
                name: "tax_code",
                table: "bakeries",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255)")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "phone",
                table: "bakeries",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255)")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "identity_card_number",
                table: "bakeries",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255)")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "email",
                table: "bakeries",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255)")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "bakery_name",
                table: "bakeries",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255)")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "available_cake_image_files",
                table: "available_cakes",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "available_cake_image_files",
                table: "available_cakes");

            migrationBuilder.AlterColumn<string>(
                name: "tax_code",
                table: "bakeries",
                type: "varchar(255)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "phone",
                table: "bakeries",
                type: "varchar(255)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "identity_card_number",
                table: "bakeries",
                type: "varchar(255)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "email",
                table: "bakeries",
                type: "varchar(255)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "bakery_name",
                table: "bakeries",
                type: "varchar(255)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<Guid>(
                name: "available_cake_image_id",
                table: "available_cakes",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci");

            migrationBuilder.CreateIndex(
                name: "IX_bakeries_bakery_name",
                table: "bakeries",
                column: "bakery_name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_bakeries_email",
                table: "bakeries",
                column: "email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_bakeries_identity_card_number",
                table: "bakeries",
                column: "identity_card_number",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_bakeries_phone",
                table: "bakeries",
                column: "phone",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_bakeries_tax_code",
                table: "bakeries",
                column: "tax_code",
                unique: true);
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CusCake.Infrastructures.Migrations
{
    /// <inheritdoc />
    public partial class update_order : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "reject_reason",
                table: "reports",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<DateTime>(
                name: "canceled_at",
                table: "orders",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "picked_up_at",
                table: "orders",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "shipping_completed_at",
                table: "orders",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "back_card_file_id",
                table: "bakeries",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci",
                oldClrType: typeof(Guid),
                oldType: "char(36)")
                .OldAnnotation("Relational:Collation", "ascii_general_ci");

            migrationBuilder.AddColumn<Guid>(
                name: "business_license_file_id",
                table: "bakeries",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<Guid>(
                name: "food_safety_certificate_file_id",
                table: "bakeries",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.CreateIndex(
                name: "IX_bakeries_business_license_file_id",
                table: "bakeries",
                column: "business_license_file_id");

            migrationBuilder.CreateIndex(
                name: "IX_bakeries_food_safety_certificate_file_id",
                table: "bakeries",
                column: "food_safety_certificate_file_id");

            migrationBuilder.AddForeignKey(
                name: "FK_bakeries_storages_business_license_file_id",
                table: "bakeries",
                column: "business_license_file_id",
                principalTable: "storages",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_bakeries_storages_food_safety_certificate_file_id",
                table: "bakeries",
                column: "food_safety_certificate_file_id",
                principalTable: "storages",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_bakeries_storages_business_license_file_id",
                table: "bakeries");

            migrationBuilder.DropForeignKey(
                name: "FK_bakeries_storages_food_safety_certificate_file_id",
                table: "bakeries");

            migrationBuilder.DropIndex(
                name: "IX_bakeries_business_license_file_id",
                table: "bakeries");

            migrationBuilder.DropIndex(
                name: "IX_bakeries_food_safety_certificate_file_id",
                table: "bakeries");

            migrationBuilder.DropColumn(
                name: "reject_reason",
                table: "reports");

            migrationBuilder.DropColumn(
                name: "canceled_at",
                table: "orders");

            migrationBuilder.DropColumn(
                name: "picked_up_at",
                table: "orders");

            migrationBuilder.DropColumn(
                name: "shipping_completed_at",
                table: "orders");

            migrationBuilder.DropColumn(
                name: "business_license_file_id",
                table: "bakeries");

            migrationBuilder.DropColumn(
                name: "food_safety_certificate_file_id",
                table: "bakeries");

            migrationBuilder.AlterColumn<Guid>(
                name: "back_card_file_id",
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

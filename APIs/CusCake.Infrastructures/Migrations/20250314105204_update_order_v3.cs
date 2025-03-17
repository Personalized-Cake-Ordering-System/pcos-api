using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CusCake.Infrastructures.Migrations
{
    /// <inheritdoc />
    public partial class update_order_v3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_customer_vouchers_vouchers_voucher_id",
                table: "customer_vouchers");

            migrationBuilder.DropIndex(
                name: "IX_customer_vouchers_voucher_id",
                table: "customer_vouchers");

            migrationBuilder.DropColumn(
                name: "discount_amount",
                table: "vouchers");

            migrationBuilder.DropColumn(
                name: "usage_limit",
                table: "vouchers");

            migrationBuilder.RenameColumn(
                name: "total_price",
                table: "orders",
                newName: "total_product_price");

            migrationBuilder.RenameColumn(
                name: "total_after_tax",
                table: "orders",
                newName: "total_customer_paid");

            migrationBuilder.RenameColumn(
                name: "tax_rate",
                table: "orders",
                newName: "shop_revenue");

            migrationBuilder.AlterColumn<DateTime>(
                name: "shipping_time",
                table: "orders",
                type: "datetime(6)",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)");

            migrationBuilder.AddColumn<double>(
                name: "app_commission_fee",
                table: "orders",
                type: "double",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "cancel_by",
                table: "orders",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<double>(
                name: "commission_rate",
                table: "orders",
                type: "double",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "voucher_code",
                table: "orders",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddForeignKey(
                name: "FK_customer_vouchers_vouchers_customer_id",
                table: "customer_vouchers",
                column: "customer_id",
                principalTable: "vouchers",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_customer_vouchers_vouchers_customer_id",
                table: "customer_vouchers");

            migrationBuilder.DropColumn(
                name: "app_commission_fee",
                table: "orders");

            migrationBuilder.DropColumn(
                name: "cancel_by",
                table: "orders");

            migrationBuilder.DropColumn(
                name: "commission_rate",
                table: "orders");

            migrationBuilder.DropColumn(
                name: "voucher_code",
                table: "orders");

            migrationBuilder.RenameColumn(
                name: "total_product_price",
                table: "orders",
                newName: "total_price");

            migrationBuilder.RenameColumn(
                name: "total_customer_paid",
                table: "orders",
                newName: "total_after_tax");

            migrationBuilder.RenameColumn(
                name: "shop_revenue",
                table: "orders",
                newName: "tax_rate");

            migrationBuilder.AddColumn<double>(
                name: "discount_amount",
                table: "vouchers",
                type: "double",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<int>(
                name: "usage_limit",
                table: "vouchers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<DateTime>(
                name: "shipping_time",
                table: "orders",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_customer_vouchers_voucher_id",
                table: "customer_vouchers",
                column: "voucher_id");

            migrationBuilder.AddForeignKey(
                name: "FK_customer_vouchers_vouchers_voucher_id",
                table: "customer_vouchers",
                column: "voucher_id",
                principalTable: "vouchers",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

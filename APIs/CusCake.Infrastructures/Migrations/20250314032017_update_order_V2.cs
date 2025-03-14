using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CusCake.Infrastructures.Migrations
{
    /// <inheritdoc />
    public partial class update_order_V2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "order_address",
                table: "orders",
                newName: "shipping_address");

            migrationBuilder.AddColumn<double>(
                name: "discount_amount",
                table: "orders",
                type: "double",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "shipping_distance",
                table: "orders",
                type: "double",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "shipping_fee",
                table: "orders",
                type: "double",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<DateTime>(
                name: "shipping_time",
                table: "orders",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<double>(
                name: "tax_rate",
                table: "orders",
                type: "double",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "total_after_tax",
                table: "orders",
                type: "double",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "discount_amount",
                table: "orders");

            migrationBuilder.DropColumn(
                name: "shipping_distance",
                table: "orders");

            migrationBuilder.DropColumn(
                name: "shipping_fee",
                table: "orders");

            migrationBuilder.DropColumn(
                name: "shipping_time",
                table: "orders");

            migrationBuilder.DropColumn(
                name: "tax_rate",
                table: "orders");

            migrationBuilder.DropColumn(
                name: "total_after_tax",
                table: "orders");

            migrationBuilder.RenameColumn(
                name: "shipping_address",
                table: "orders",
                newName: "order_address");
        }
    }
}

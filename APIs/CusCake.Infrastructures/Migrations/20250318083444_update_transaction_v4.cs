using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CusCake.Infrastructures.Migrations
{
    /// <inheritdoc />
    public partial class update_transaction_v4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_orders_transactions_transaction_id",
                table: "orders");

            migrationBuilder.DropIndex(
                name: "IX_orders_transaction_id",
                table: "orders");

            migrationBuilder.DropColumn(
                name: "transaction_id",
                table: "orders");

            migrationBuilder.AddColumn<Guid>(
                name: "order_id",
                table: "transactions",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci");

            migrationBuilder.CreateIndex(
                name: "IX_transactions_order_id",
                table: "transactions",
                column: "order_id");

            migrationBuilder.AddForeignKey(
                name: "FK_transactions_orders_order_id",
                table: "transactions",
                column: "order_id",
                principalTable: "orders",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_transactions_orders_order_id",
                table: "transactions");

            migrationBuilder.DropIndex(
                name: "IX_transactions_order_id",
                table: "transactions");

            migrationBuilder.DropColumn(
                name: "order_id",
                table: "transactions");

            migrationBuilder.AddColumn<Guid>(
                name: "transaction_id",
                table: "orders",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.CreateIndex(
                name: "IX_orders_transaction_id",
                table: "orders",
                column: "transaction_id");

            migrationBuilder.AddForeignKey(
                name: "FK_orders_transactions_transaction_id",
                table: "orders",
                column: "transaction_id",
                principalTable: "transactions",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

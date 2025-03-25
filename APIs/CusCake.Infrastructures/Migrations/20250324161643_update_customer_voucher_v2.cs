using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CusCake.Infrastructures.Migrations
{
    /// <inheritdoc />
    public partial class update_customer_voucher_v2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_customer_vouchers_vouchers_customer_id",
                table: "customer_vouchers");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_customer_vouchers_vouchers_voucher_id",
                table: "customer_vouchers");

            migrationBuilder.DropIndex(
                name: "IX_customer_vouchers_voucher_id",
                table: "customer_vouchers");

            migrationBuilder.AddForeignKey(
                name: "FK_customer_vouchers_vouchers_customer_id",
                table: "customer_vouchers",
                column: "customer_id",
                principalTable: "vouchers",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

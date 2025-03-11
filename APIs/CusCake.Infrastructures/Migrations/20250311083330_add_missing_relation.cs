using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CusCake.Infrastructures.Migrations
{
    /// <inheritdoc />
    public partial class add_missing_relation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "admin_id",
                table: "auths",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<Guid>(
                name: "bakery_id",
                table: "auths",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<Guid>(
                name: "customer_id",
                table: "auths",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.CreateIndex(
                name: "IX_cake_part_options_bakery_id",
                table: "cake_part_options",
                column: "bakery_id");

            migrationBuilder.CreateIndex(
                name: "IX_cake_message_options_bakery_id",
                table: "cake_message_options",
                column: "bakery_id");

            migrationBuilder.CreateIndex(
                name: "IX_cake_extra_options_bakery_id",
                table: "cake_extra_options",
                column: "bakery_id");

            migrationBuilder.CreateIndex(
                name: "IX_cake_decoration_options_bakery_id",
                table: "cake_decoration_options",
                column: "bakery_id");

            migrationBuilder.CreateIndex(
                name: "IX_auths_admin_id",
                table: "auths",
                column: "admin_id");

            migrationBuilder.CreateIndex(
                name: "IX_auths_bakery_id",
                table: "auths",
                column: "bakery_id");

            migrationBuilder.CreateIndex(
                name: "IX_auths_customer_id",
                table: "auths",
                column: "customer_id");

            migrationBuilder.AddForeignKey(
                name: "FK_auths_admins_admin_id",
                table: "auths",
                column: "admin_id",
                principalTable: "admins",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_auths_bakeries_bakery_id",
                table: "auths",
                column: "bakery_id",
                principalTable: "bakeries",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_auths_customers_customer_id",
                table: "auths",
                column: "customer_id",
                principalTable: "customers",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_cake_decoration_options_bakeries_bakery_id",
                table: "cake_decoration_options",
                column: "bakery_id",
                principalTable: "bakeries",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_cake_extra_options_bakeries_bakery_id",
                table: "cake_extra_options",
                column: "bakery_id",
                principalTable: "bakeries",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_cake_message_options_bakeries_bakery_id",
                table: "cake_message_options",
                column: "bakery_id",
                principalTable: "bakeries",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_cake_part_options_bakeries_bakery_id",
                table: "cake_part_options",
                column: "bakery_id",
                principalTable: "bakeries",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_auths_admins_admin_id",
                table: "auths");

            migrationBuilder.DropForeignKey(
                name: "FK_auths_bakeries_bakery_id",
                table: "auths");

            migrationBuilder.DropForeignKey(
                name: "FK_auths_customers_customer_id",
                table: "auths");

            migrationBuilder.DropForeignKey(
                name: "FK_cake_decoration_options_bakeries_bakery_id",
                table: "cake_decoration_options");

            migrationBuilder.DropForeignKey(
                name: "FK_cake_extra_options_bakeries_bakery_id",
                table: "cake_extra_options");

            migrationBuilder.DropForeignKey(
                name: "FK_cake_message_options_bakeries_bakery_id",
                table: "cake_message_options");

            migrationBuilder.DropForeignKey(
                name: "FK_cake_part_options_bakeries_bakery_id",
                table: "cake_part_options");

            migrationBuilder.DropIndex(
                name: "IX_cake_part_options_bakery_id",
                table: "cake_part_options");

            migrationBuilder.DropIndex(
                name: "IX_cake_message_options_bakery_id",
                table: "cake_message_options");

            migrationBuilder.DropIndex(
                name: "IX_cake_extra_options_bakery_id",
                table: "cake_extra_options");

            migrationBuilder.DropIndex(
                name: "IX_cake_decoration_options_bakery_id",
                table: "cake_decoration_options");

            migrationBuilder.DropIndex(
                name: "IX_auths_admin_id",
                table: "auths");

            migrationBuilder.DropIndex(
                name: "IX_auths_bakery_id",
                table: "auths");

            migrationBuilder.DropIndex(
                name: "IX_auths_customer_id",
                table: "auths");

            migrationBuilder.DropColumn(
                name: "admin_id",
                table: "auths");

            migrationBuilder.DropColumn(
                name: "bakery_id",
                table: "auths");

            migrationBuilder.DropColumn(
                name: "customer_id",
                table: "auths");
        }
    }
}

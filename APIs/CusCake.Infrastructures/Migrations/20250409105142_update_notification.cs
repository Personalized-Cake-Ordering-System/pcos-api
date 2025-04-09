using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CusCake.Infrastructures.Migrations
{
    /// <inheritdoc />
    public partial class update_notification : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // migrationBuilder.RenameColumn(
            //     name: "baned_at",
            //     table: "bakeries",
            //     newName: "banned_at");

            migrationBuilder.AddColumn<Guid>(
                name: "admin_id",
                table: "notifications",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.CreateIndex(
                name: "IX_notifications_admin_id",
                table: "notifications",
                column: "admin_id");

            migrationBuilder.AddForeignKey(
                name: "FK_notifications_admins_admin_id",
                table: "notifications",
                column: "admin_id",
                principalTable: "admins",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_notifications_admins_admin_id",
                table: "notifications");

            migrationBuilder.DropIndex(
                name: "IX_notifications_admin_id",
                table: "notifications");

            migrationBuilder.DropColumn(
                name: "admin_id",
                table: "notifications");

            migrationBuilder.RenameColumn(
                name: "banned_at",
                table: "bakeries",
                newName: "baned_at");
        }
    }
}

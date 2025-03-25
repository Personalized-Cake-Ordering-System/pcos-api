using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CusCake.Infrastructures.Migrations
{
    /// <inheritdoc />
    public partial class update_cake_review : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "review_image_file_id",
                table: "cake_reviews");

            migrationBuilder.AddColumn<Guid>(
                name: "image_id",
                table: "cake_reviews",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.CreateIndex(
                name: "IX_cake_reviews_image_id",
                table: "cake_reviews",
                column: "image_id");

            migrationBuilder.AddForeignKey(
                name: "FK_cake_reviews_storages_image_id",
                table: "cake_reviews",
                column: "image_id",
                principalTable: "storages",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_cake_reviews_storages_image_id",
                table: "cake_reviews");

            migrationBuilder.DropIndex(
                name: "IX_cake_reviews_image_id",
                table: "cake_reviews");

            migrationBuilder.DropColumn(
                name: "image_id",
                table: "cake_reviews");

            migrationBuilder.AddColumn<Guid>(
                name: "review_image_file_id",
                table: "cake_reviews",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci");
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CusCake.Infrastructures.Migrations
{
    /// <inheritdoc />
    public partial class update_cake_part_and_extra : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_cake_extras_storages_extra_image_id",
                table: "cake_extras");

            migrationBuilder.DropForeignKey(
                name: "FK_cake_parts_storages_part_image_id",
                table: "cake_parts");

            migrationBuilder.AlterColumn<Guid>(
                name: "part_image_id",
                table: "cake_parts",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci",
                oldClrType: typeof(Guid),
                oldType: "char(36)")
                .OldAnnotation("Relational:Collation", "ascii_general_ci");

            migrationBuilder.AlterColumn<Guid>(
                name: "extra_image_id",
                table: "cake_extras",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci",
                oldClrType: typeof(Guid),
                oldType: "char(36)")
                .OldAnnotation("Relational:Collation", "ascii_general_ci");

            migrationBuilder.AddForeignKey(
                name: "FK_cake_extras_storages_extra_image_id",
                table: "cake_extras",
                column: "extra_image_id",
                principalTable: "storages",
                principalColumn: "id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_cake_parts_storages_part_image_id",
                table: "cake_parts",
                column: "part_image_id",
                principalTable: "storages",
                principalColumn: "id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_cake_extras_storages_extra_image_id",
                table: "cake_extras");

            migrationBuilder.DropForeignKey(
                name: "FK_cake_parts_storages_part_image_id",
                table: "cake_parts");

            migrationBuilder.AlterColumn<Guid>(
                name: "part_image_id",
                table: "cake_parts",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci",
                oldClrType: typeof(Guid),
                oldType: "char(36)",
                oldNullable: true)
                .OldAnnotation("Relational:Collation", "ascii_general_ci");

            migrationBuilder.AlterColumn<Guid>(
                name: "extra_image_id",
                table: "cake_extras",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci",
                oldClrType: typeof(Guid),
                oldType: "char(36)",
                oldNullable: true)
                .OldAnnotation("Relational:Collation", "ascii_general_ci");

            migrationBuilder.AddForeignKey(
                name: "FK_cake_extras_storages_extra_image_id",
                table: "cake_extras",
                column: "extra_image_id",
                principalTable: "storages",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_cake_parts_storages_part_image_id",
                table: "cake_parts",
                column: "part_image_id",
                principalTable: "storages",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

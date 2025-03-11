using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CusCake.Infrastructures.Migrations
{
    /// <inheritdoc />
    public partial class update_bakery : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            // migrationBuilder.AddForeignKey(
            //     name: "FK_custom_cakes_cake_message_selections_message_selection_id",
            //     table: "custom_cakes",
            //     column: "message_selection_id",
            //     principalTable: "cake_message_selections",
            //     principalColumn: "id",
            //     onDelete: ReferentialAction.Cascade);

            // migrationBuilder.AddPrimaryKey(
            //     name: "PK_cake_message_selections",
            //     table: "cake_message_selections",
            //     column: "id");


            // migrationBuilder.DropForeignKey(
            //     name: "FK_custom_cakes_CakeMessageSelections_message_selection_id",
            //     table: "custom_cakes");

            // migrationBuilder.DropPrimaryKey(
            //     name: "PK_CakeMessageSelections",
            //     table: "CakeMessageSelections");


            migrationBuilder.DropColumn(
                name: "shop_image_files",
                table: "bakeries");

            migrationBuilder.DropColumn(
                name: "available_cake_image_files",
                table: "available_cakes");

            migrationBuilder.RenameTable(
                name: "CakeMessageSelections",
                newName: "cake_message_selections");

            migrationBuilder.AddColumn<Guid>(
                name: "AvailableCakeId",
                table: "storages",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<Guid>(
                name: "BakeryId",
                table: "storages",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.CreateIndex(
                name: "IX_storages_AvailableCakeId",
                table: "storages",
                column: "AvailableCakeId");

            migrationBuilder.CreateIndex(
                name: "IX_storages_BakeryId",
                table: "storages",
                column: "BakeryId");



            migrationBuilder.AddForeignKey(
                name: "FK_storages_available_cakes_AvailableCakeId",
                table: "storages",
                column: "AvailableCakeId",
                principalTable: "available_cakes",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_storages_bakeries_BakeryId",
                table: "storages",
                column: "BakeryId",
                principalTable: "bakeries",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // migrationBuilder.DropForeignKey(
            //     name: "FK_custom_cakes_cake_message_selections_message_selection_id",
            //     table: "custom_cakes");

            migrationBuilder.DropForeignKey(
                name: "FK_storages_available_cakes_AvailableCakeId",
                table: "storages");

            migrationBuilder.DropForeignKey(
                name: "FK_storages_bakeries_BakeryId",
                table: "storages");

            migrationBuilder.DropIndex(
                name: "IX_storages_AvailableCakeId",
                table: "storages");

            migrationBuilder.DropIndex(
                name: "IX_storages_BakeryId",
                table: "storages");

            // migrationBuilder.DropPrimaryKey(
            //     name: "PK_cake_message_selections",
            //     table: "cake_message_selections");

            migrationBuilder.DropColumn(
                name: "AvailableCakeId",
                table: "storages");

            migrationBuilder.DropColumn(
                name: "BakeryId",
                table: "storages");

            migrationBuilder.RenameTable(
                name: "cake_message_selections",
                newName: "CakeMessageSelections");

            migrationBuilder.AddColumn<string>(
                name: "shop_image_files",
                table: "bakeries",
                type: "json",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "available_cake_image_files",
                table: "available_cakes",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            // migrationBuilder.AddPrimaryKey(
            //     name: "PK_CakeMessageSelections",
            //     table: "CakeMessageSelections",
            //     column: "id");

            // migrationBuilder.AddForeignKey(
            //     name: "FK_custom_cakes_CakeMessageSelections_message_selection_id",
            //     table: "custom_cakes",
            //     column: "message_selection_id",
            //     principalTable: "CakeMessageSelections",
            //     principalColumn: "id",
            //     onDelete: ReferentialAction.Cascade);
        }
    }
}

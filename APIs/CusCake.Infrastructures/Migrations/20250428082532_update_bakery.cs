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
            migrationBuilder.AddColumn<TimeSpan>(
                name: "close_time",
                table: "bakeries",
                type: "time(6)",
                nullable: true);

            migrationBuilder.AddColumn<TimeSpan>(
                name: "open_time",
                table: "bakeries",
                type: "time(6)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "close_time",
                table: "bakeries");

            migrationBuilder.DropColumn(
                name: "open_time",
                table: "bakeries");
        }
    }
}

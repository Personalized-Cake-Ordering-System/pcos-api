using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CusCake.Infrastructures.Migrations
{
    /// <inheritdoc />
    public partial class update_order_v4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "bank_events");

            migrationBuilder.RenameColumn(
                name: "order_support_file_id",
                table: "order_supports",
                newName: "file_id");

            migrationBuilder.RenameColumn(
                name: "sender_id",
                table: "notifications",
                newName: "target_entity_id");

            migrationBuilder.RenameColumn(
                name: "notification_type",
                table: "notifications",
                newName: "sender_type");

            migrationBuilder.AddColumn<string>(
                name: "account_number",
                table: "transactions",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<double>(
                name: "accumulated",
                table: "transactions",
                type: "double",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "code",
                table: "transactions",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "content",
                table: "transactions",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "description",
                table: "transactions",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "reference_code",
                table: "transactions",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "sub_account",
                table: "transactions",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<double>(
                name: "transfer_amount",
                table: "transactions",
                type: "double",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "transfer_type",
                table: "transactions",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<double>(
                name: "shipping_time",
                table: "orders",
                type: "double",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "order_code",
                table: "orders",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<DateTime>(
                name: "paid_at",
                table: "orders",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "OrderId1",
                table: "order_supports",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "bank_account",
                table: "bakeries",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<Guid>(
                name: "wallet_id",
                table: "auths",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci");

            migrationBuilder.CreateTable(
                name: "wallets",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    balance = table.Column<double>(type: "double", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    created_by = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    updated_at = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    updated_by = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    is_deleted = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_wallets", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "wallet_transactions",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    wallet_id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    amount = table.Column<double>(type: "double", nullable: false),
                    transaction_type = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    created_at = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    created_by = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    updated_at = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    updated_by = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    is_deleted = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_wallet_transactions", x => x.id);
                    table.ForeignKey(
                        name: "FK_wallet_transactions_wallets_wallet_id",
                        column: x => x.wallet_id,
                        principalTable: "wallets",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_order_supports_file_id",
                table: "order_supports",
                column: "file_id");

            // migrationBuilder.CreateIndex(
            //     name: "IX_order_supports_OrderId1",
            //     table: "order_supports",
            //     column: "OrderId1");

            migrationBuilder.CreateIndex(
                name: "IX_auths_wallet_id",
                table: "auths",
                column: "wallet_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_wallet_transactions_wallet_id",
                table: "wallet_transactions",
                column: "wallet_id");

            migrationBuilder.AddForeignKey(
                name: "FK_auths_wallets_wallet_id",
                table: "auths",
                column: "wallet_id",
                principalTable: "wallets",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            // migrationBuilder.AddForeignKey(
            //     name: "FK_order_supports_orders_OrderId1",
            //     table: "order_supports",
            //     column: "OrderId1",
            //     principalTable: "orders",
            //     principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_order_supports_storages_file_id",
                table: "order_supports",
                column: "file_id",
                principalTable: "storages",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_auths_wallets_wallet_id",
                table: "auths");

            migrationBuilder.DropForeignKey(
                name: "FK_order_supports_orders_OrderId1",
                table: "order_supports");

            migrationBuilder.DropForeignKey(
                name: "FK_order_supports_storages_file_id",
                table: "order_supports");

            migrationBuilder.DropTable(
                name: "wallet_transactions");

            migrationBuilder.DropTable(
                name: "wallets");

            migrationBuilder.DropIndex(
                name: "IX_order_supports_file_id",
                table: "order_supports");

            migrationBuilder.DropIndex(
                name: "IX_order_supports_OrderId1",
                table: "order_supports");

            migrationBuilder.DropIndex(
                name: "IX_auths_wallet_id",
                table: "auths");

            migrationBuilder.DropColumn(
                name: "account_number",
                table: "transactions");

            migrationBuilder.DropColumn(
                name: "accumulated",
                table: "transactions");

            migrationBuilder.DropColumn(
                name: "code",
                table: "transactions");

            migrationBuilder.DropColumn(
                name: "content",
                table: "transactions");

            migrationBuilder.DropColumn(
                name: "description",
                table: "transactions");

            migrationBuilder.DropColumn(
                name: "reference_code",
                table: "transactions");

            migrationBuilder.DropColumn(
                name: "sub_account",
                table: "transactions");

            migrationBuilder.DropColumn(
                name: "transfer_amount",
                table: "transactions");

            migrationBuilder.DropColumn(
                name: "transfer_type",
                table: "transactions");

            migrationBuilder.DropColumn(
                name: "order_code",
                table: "orders");

            migrationBuilder.DropColumn(
                name: "paid_at",
                table: "orders");

            // migrationBuilder.DropColumn(
            //     name: "OrderId1",
            //     table: "order_supports");

            migrationBuilder.DropColumn(
                name: "wallet_id",
                table: "auths");

            migrationBuilder.RenameColumn(
                name: "file_id",
                table: "order_supports",
                newName: "order_support_file_id");

            migrationBuilder.RenameColumn(
                name: "target_entity_id",
                table: "notifications",
                newName: "sender_id");

            migrationBuilder.RenameColumn(
                name: "sender_type",
                table: "notifications",
                newName: "notification_type");

            migrationBuilder.AlterColumn<DateTime>(
                name: "shipping_time",
                table: "orders",
                type: "datetime(6)",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "double",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "bakeries",
                keyColumn: "bank_account",
                keyValue: null,
                column: "bank_account",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "bank_account",
                table: "bakeries",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "bank_events",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    transaction_id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    account_number = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    accumulated = table.Column<double>(type: "double", nullable: false),
                    code = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    content = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    created_at = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    created_by = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    description = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    gateway = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    is_deleted = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    is_processed = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    processed_at = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    reference_code = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    sub_account = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    transaction_date = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    tranfer_amount = table.Column<double>(type: "double", nullable: false),
                    transfer_type = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    updated_at = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    updated_by = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_bank_events", x => x.id);
                    table.ForeignKey(
                        name: "FK_bank_events_transactions_transaction_id",
                        column: x => x.transaction_id,
                        principalTable: "transactions",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_bank_events_transaction_id",
                table: "bank_events",
                column: "transaction_id");
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CusCake.Infrastructures.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "admins",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    name = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    email = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    password = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    created_at = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    created_by = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    updated_at = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    updated_by = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    is_deleted = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_admins", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "customers",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    phone = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    address = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    latitude = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    longitude = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    email = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    password = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    account_type = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    created_at = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    created_by = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    updated_at = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    updated_by = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    is_deleted = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_customers", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "storages",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    file_name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    file_url = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    created_at = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    created_by = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    updated_at = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    updated_by = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    is_deleted = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_storages", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

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
                name: "bakeries",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    bakery_name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    email = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    password = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    phone = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    address = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    latitude = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    longitude = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    bank_account = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    owner_name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    avatar_file_id = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    identity_card_number = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    font_card_file_id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    back_card_file_id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    tax_code = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    status = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    confirmed_at = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    shop_image_files = table.Column<string>(type: "json", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    created_at = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    created_by = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    updated_at = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    updated_by = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    is_deleted = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_bakeries", x => x.id);
                    table.ForeignKey(
                        name: "FK_bakeries_storages_avatar_file_id",
                        column: x => x.avatar_file_id,
                        principalTable: "storages",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_bakeries_storages_back_card_file_id",
                        column: x => x.back_card_file_id,
                        principalTable: "storages",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_bakeries_storages_font_card_file_id",
                        column: x => x.font_card_file_id,
                        principalTable: "storages",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "cake_message_selections",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    text = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    message_type = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    image_id = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    message_options = table.Column<string>(type: "json", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    created_at = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    created_by = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    updated_at = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    updated_by = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    is_deleted = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cake_message_selections", x => x.id);
                    table.ForeignKey(
                        name: "FK_cake_message_selections_storages_image_id",
                        column: x => x.image_id,
                        principalTable: "storages",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
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

            migrationBuilder.CreateTable(
                name: "auths",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    email = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    password = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    role = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    entity_id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    bakery_id = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    customer_id = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    admin_id = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    wallet_id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    created_at = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    created_by = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    updated_at = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    updated_by = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    is_deleted = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_auths", x => x.id);
                    table.ForeignKey(
                        name: "FK_auths_admins_admin_id",
                        column: x => x.admin_id,
                        principalTable: "admins",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_auths_bakeries_bakery_id",
                        column: x => x.bakery_id,
                        principalTable: "bakeries",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_auths_customers_customer_id",
                        column: x => x.customer_id,
                        principalTable: "customers",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_auths_wallets_wallet_id",
                        column: x => x.wallet_id,
                        principalTable: "wallets",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "available_cakes",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    available_cake_price = table.Column<double>(type: "double", nullable: false),
                    available_cake_name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    available_cake_description = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    available_cake_type = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    available_cake_quantity = table.Column<int>(type: "int", nullable: false),
                    available_main_image_id = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    available_cake_image_files = table.Column<string>(type: "json", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    bakery_id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    created_at = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    created_by = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    updated_at = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    updated_by = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    is_deleted = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_available_cakes", x => x.id);
                    table.ForeignKey(
                        name: "FK_available_cakes_bakeries_bakery_id",
                        column: x => x.bakery_id,
                        principalTable: "bakeries",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_available_cakes_storages_available_main_image_id",
                        column: x => x.available_main_image_id,
                        principalTable: "storages",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "cake_decoration_options",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    price = table.Column<double>(type: "double", nullable: false),
                    color = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    is_default = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    description = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    image_id = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    type = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    bakery_id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    created_at = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    created_by = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    updated_at = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    updated_by = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    is_deleted = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cake_decoration_options", x => x.id);
                    table.ForeignKey(
                        name: "FK_cake_decoration_options_bakeries_bakery_id",
                        column: x => x.bakery_id,
                        principalTable: "bakeries",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_cake_decoration_options_storages_image_id",
                        column: x => x.image_id,
                        principalTable: "storages",
                        principalColumn: "id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "cake_extra_options",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    price = table.Column<double>(type: "double", nullable: false),
                    color = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    is_default = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    description = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    image_id = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    type = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    bakery_id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    created_at = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    created_by = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    updated_at = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    updated_by = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    is_deleted = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cake_extra_options", x => x.id);
                    table.ForeignKey(
                        name: "FK_cake_extra_options_bakeries_bakery_id",
                        column: x => x.bakery_id,
                        principalTable: "bakeries",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_cake_extra_options_storages_image_id",
                        column: x => x.image_id,
                        principalTable: "storages",
                        principalColumn: "id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "cake_message_options",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    type = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    color = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    bakery_id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    created_at = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    created_by = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    updated_at = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    updated_by = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    is_deleted = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cake_message_options", x => x.id);
                    table.ForeignKey(
                        name: "FK_cake_message_options_bakeries_bakery_id",
                        column: x => x.bakery_id,
                        principalTable: "bakeries",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "cake_part_options",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    price = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    color = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    description = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    is_default = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    image_id = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    type = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    bakery_id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    created_at = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    created_by = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    updated_at = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    updated_by = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    is_deleted = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cake_part_options", x => x.id);
                    table.ForeignKey(
                        name: "FK_cake_part_options_bakeries_bakery_id",
                        column: x => x.bakery_id,
                        principalTable: "bakeries",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_cake_part_options_storages_image_id",
                        column: x => x.image_id,
                        principalTable: "storages",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "notifications",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    title = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    content = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    sender_type = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    type = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    is_read = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    target_entity_id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    bakery_id = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    customer_id = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    created_at = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    created_by = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    updated_at = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    updated_by = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    is_deleted = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_notifications", x => x.id);
                    table.ForeignKey(
                        name: "FK_notifications_bakeries_bakery_id",
                        column: x => x.bakery_id,
                        principalTable: "bakeries",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_notifications_customers_customer_id",
                        column: x => x.customer_id,
                        principalTable: "customers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "vouchers",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    bakery_id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    code = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    discount_percentage = table.Column<double>(type: "double", nullable: false),
                    min_order_amount = table.Column<double>(type: "double", nullable: false),
                    max_discount_amount = table.Column<double>(type: "double", nullable: false),
                    expiration_date = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    quantity = table.Column<int>(type: "int", nullable: false),
                    usage_count = table.Column<int>(type: "int", nullable: false),
                    description = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    voucher_type = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    created_at = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    created_by = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    updated_at = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    updated_by = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    is_deleted = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_vouchers", x => x.id);
                    table.ForeignKey(
                        name: "FK_vouchers_bakeries_bakery_id",
                        column: x => x.bakery_id,
                        principalTable: "bakeries",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "custom_cakes",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    total_price = table.Column<double>(type: "double", nullable: false),
                    custom_cake_name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    custom_cake_description = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    recipe = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    customer_id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    bakery_id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    message_selection_id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    created_at = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    created_by = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    updated_at = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    updated_by = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    is_deleted = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_custom_cakes", x => x.id);
                    table.ForeignKey(
                        name: "FK_custom_cakes_bakeries_bakery_id",
                        column: x => x.bakery_id,
                        principalTable: "bakeries",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_custom_cakes_cake_message_selections_message_selection_id",
                        column: x => x.message_selection_id,
                        principalTable: "cake_message_selections",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_custom_cakes_customers_customer_id",
                        column: x => x.customer_id,
                        principalTable: "customers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "cake_reviews",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    content = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    rating = table.Column<int>(type: "int", nullable: false),
                    review_image_file_id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    order_detail_id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    available_cake_id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    bakery_id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    customer_id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    created_at = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    created_by = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    updated_at = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    updated_by = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    is_deleted = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cake_reviews", x => x.id);
                    table.ForeignKey(
                        name: "FK_cake_reviews_available_cakes_available_cake_id",
                        column: x => x.available_cake_id,
                        principalTable: "available_cakes",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_cake_reviews_bakeries_bakery_id",
                        column: x => x.bakery_id,
                        principalTable: "bakeries",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_cake_reviews_customers_customer_id",
                        column: x => x.customer_id,
                        principalTable: "customers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "orders",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    total_product_price = table.Column<double>(type: "double", nullable: false),
                    total_customer_paid = table.Column<double>(type: "double", nullable: false),
                    shipping_distance = table.Column<double>(type: "double", nullable: false),
                    discount_amount = table.Column<double>(type: "double", nullable: false),
                    shipping_fee = table.Column<double>(type: "double", nullable: false),
                    shipping_time = table.Column<double>(type: "double", nullable: true),
                    shipping_type = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    commission_rate = table.Column<double>(type: "double", nullable: false),
                    app_commission_fee = table.Column<double>(type: "double", nullable: false),
                    shop_revenue = table.Column<double>(type: "double", nullable: false),
                    voucher_code = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    order_note = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    pickup_time = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    payment_type = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    canceled_reason = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    phone_number = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    shipping_address = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    latitude = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    longitude = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    order_status = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    cancel_by = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    order_code = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    paid_at = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    customer_id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    bakery_id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    transaction_id = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    voucher_id = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    customer_voucher_id = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    created_at = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    created_by = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    updated_at = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    updated_by = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    is_deleted = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_orders", x => x.id);
                    table.ForeignKey(
                        name: "FK_orders_bakeries_bakery_id",
                        column: x => x.bakery_id,
                        principalTable: "bakeries",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_orders_customers_customer_id",
                        column: x => x.customer_id,
                        principalTable: "customers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_orders_vouchers_voucher_id",
                        column: x => x.voucher_id,
                        principalTable: "vouchers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "cake_decoration_selections",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    custom_cake_id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    decoration_type = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    decoration_option_id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    created_at = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    created_by = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    updated_at = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    updated_by = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    is_deleted = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cake_decoration_selections", x => x.id);
                    table.ForeignKey(
                        name: "FK_cake_decoration_selections_cake_decoration_options_decoratio~",
                        column: x => x.decoration_option_id,
                        principalTable: "cake_decoration_options",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_cake_decoration_selections_custom_cakes_custom_cake_id",
                        column: x => x.custom_cake_id,
                        principalTable: "custom_cakes",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "cake_extra_selections",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    custom_cake_id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    extra_type = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    extra_option_id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    created_at = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    created_by = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    updated_at = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    updated_by = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    is_deleted = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cake_extra_selections", x => x.id);
                    table.ForeignKey(
                        name: "FK_cake_extra_selections_cake_extra_options_extra_option_id",
                        column: x => x.extra_option_id,
                        principalTable: "cake_extra_options",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_cake_extra_selections_custom_cakes_custom_cake_id",
                        column: x => x.custom_cake_id,
                        principalTable: "custom_cakes",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "cake_part_selections",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    part_type = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    custom_cake_id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    part_option_id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    created_at = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    created_by = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    updated_at = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    updated_by = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    is_deleted = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cake_part_selections", x => x.id);
                    table.ForeignKey(
                        name: "FK_cake_part_selections_cake_part_options_part_option_id",
                        column: x => x.part_option_id,
                        principalTable: "cake_part_options",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_cake_part_selections_custom_cakes_custom_cake_id",
                        column: x => x.custom_cake_id,
                        principalTable: "custom_cakes",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "customer_vouchers",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    customer_id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    voucher_id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    oder_id = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    created_at = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    created_by = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    updated_at = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    updated_by = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    is_deleted = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_customer_vouchers", x => x.id);
                    table.ForeignKey(
                        name: "FK_customer_vouchers_customers_customer_id",
                        column: x => x.customer_id,
                        principalTable: "customers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_customer_vouchers_orders_oder_id",
                        column: x => x.oder_id,
                        principalTable: "orders",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_customer_vouchers_vouchers_customer_id",
                        column: x => x.customer_id,
                        principalTable: "vouchers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "order_detail",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    order_id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    available_cake_id = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    cake_note = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    sub_total_price = table.Column<double>(type: "double", nullable: true),
                    cake_review_id = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    custom_cake_id = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    created_at = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    created_by = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    updated_at = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    updated_by = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    is_deleted = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_order_detail", x => x.id);
                    table.ForeignKey(
                        name: "FK_order_detail_available_cakes_available_cake_id",
                        column: x => x.available_cake_id,
                        principalTable: "available_cakes",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_order_detail_cake_reviews_cake_review_id",
                        column: x => x.cake_review_id,
                        principalTable: "cake_reviews",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_order_detail_custom_cakes_custom_cake_id",
                        column: x => x.custom_cake_id,
                        principalTable: "custom_cakes",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_order_detail_orders_order_id",
                        column: x => x.order_id,
                        principalTable: "orders",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "order_supports",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    content = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    file_id = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    bakery_id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    customer_id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    order_id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    created_at = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    created_by = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    updated_at = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    updated_by = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    is_deleted = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_order_supports", x => x.id);
                    table.ForeignKey(
                        name: "FK_order_supports_bakeries_bakery_id",
                        column: x => x.bakery_id,
                        principalTable: "bakeries",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_order_supports_customers_customer_id",
                        column: x => x.customer_id,
                        principalTable: "customers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_order_supports_orders_order_id",
                        column: x => x.order_id,
                        principalTable: "orders",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_order_supports_storages_file_id",
                        column: x => x.file_id,
                        principalTable: "storages",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "transactions",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    amount = table.Column<double>(type: "double", nullable: false),
                    order_id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    account_number = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    sub_account = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    code = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    content = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    transfer_type = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    description = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    transfer_amount = table.Column<double>(type: "double", nullable: false),
                    reference_code = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    accumulated = table.Column<double>(type: "double", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    created_by = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    updated_at = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    updated_by = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    is_deleted = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_transactions", x => x.id);
                    table.ForeignKey(
                        name: "FK_transactions_orders_order_id",
                        column: x => x.order_id,
                        principalTable: "orders",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_admins_email",
                table: "admins",
                column: "email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_admins_name",
                table: "admins",
                column: "name",
                unique: true);

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

            migrationBuilder.CreateIndex(
                name: "IX_auths_wallet_id",
                table: "auths",
                column: "wallet_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_available_cakes_available_main_image_id",
                table: "available_cakes",
                column: "available_main_image_id");

            migrationBuilder.CreateIndex(
                name: "IX_available_cakes_bakery_id",
                table: "available_cakes",
                column: "bakery_id");

            migrationBuilder.CreateIndex(
                name: "IX_bakeries_avatar_file_id",
                table: "bakeries",
                column: "avatar_file_id");

            migrationBuilder.CreateIndex(
                name: "IX_bakeries_back_card_file_id",
                table: "bakeries",
                column: "back_card_file_id");

            migrationBuilder.CreateIndex(
                name: "IX_bakeries_font_card_file_id",
                table: "bakeries",
                column: "font_card_file_id");

            migrationBuilder.CreateIndex(
                name: "IX_cake_decoration_options_bakery_id",
                table: "cake_decoration_options",
                column: "bakery_id");

            migrationBuilder.CreateIndex(
                name: "IX_cake_decoration_options_image_id",
                table: "cake_decoration_options",
                column: "image_id");

            migrationBuilder.CreateIndex(
                name: "IX_cake_decoration_selections_custom_cake_id",
                table: "cake_decoration_selections",
                column: "custom_cake_id");

            migrationBuilder.CreateIndex(
                name: "IX_cake_decoration_selections_decoration_option_id",
                table: "cake_decoration_selections",
                column: "decoration_option_id");

            migrationBuilder.CreateIndex(
                name: "IX_cake_extra_options_bakery_id",
                table: "cake_extra_options",
                column: "bakery_id");

            migrationBuilder.CreateIndex(
                name: "IX_cake_extra_options_image_id",
                table: "cake_extra_options",
                column: "image_id");

            migrationBuilder.CreateIndex(
                name: "IX_cake_extra_selections_custom_cake_id",
                table: "cake_extra_selections",
                column: "custom_cake_id");

            migrationBuilder.CreateIndex(
                name: "IX_cake_extra_selections_extra_option_id",
                table: "cake_extra_selections",
                column: "extra_option_id");

            migrationBuilder.CreateIndex(
                name: "IX_cake_message_options_bakery_id",
                table: "cake_message_options",
                column: "bakery_id");

            migrationBuilder.CreateIndex(
                name: "IX_cake_message_selections_image_id",
                table: "cake_message_selections",
                column: "image_id");

            migrationBuilder.CreateIndex(
                name: "IX_cake_part_options_bakery_id",
                table: "cake_part_options",
                column: "bakery_id");

            migrationBuilder.CreateIndex(
                name: "IX_cake_part_options_image_id",
                table: "cake_part_options",
                column: "image_id");

            migrationBuilder.CreateIndex(
                name: "IX_cake_part_selections_custom_cake_id",
                table: "cake_part_selections",
                column: "custom_cake_id");

            migrationBuilder.CreateIndex(
                name: "IX_cake_part_selections_part_option_id",
                table: "cake_part_selections",
                column: "part_option_id");

            migrationBuilder.CreateIndex(
                name: "IX_cake_reviews_available_cake_id",
                table: "cake_reviews",
                column: "available_cake_id");

            migrationBuilder.CreateIndex(
                name: "IX_cake_reviews_bakery_id",
                table: "cake_reviews",
                column: "bakery_id");

            migrationBuilder.CreateIndex(
                name: "IX_cake_reviews_customer_id",
                table: "cake_reviews",
                column: "customer_id");

            migrationBuilder.CreateIndex(
                name: "IX_custom_cakes_bakery_id",
                table: "custom_cakes",
                column: "bakery_id");

            migrationBuilder.CreateIndex(
                name: "IX_custom_cakes_customer_id",
                table: "custom_cakes",
                column: "customer_id");

            migrationBuilder.CreateIndex(
                name: "IX_custom_cakes_message_selection_id",
                table: "custom_cakes",
                column: "message_selection_id");

            migrationBuilder.CreateIndex(
                name: "IX_customer_vouchers_customer_id",
                table: "customer_vouchers",
                column: "customer_id");

            migrationBuilder.CreateIndex(
                name: "IX_customer_vouchers_oder_id",
                table: "customer_vouchers",
                column: "oder_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_customers_email",
                table: "customers",
                column: "email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_notifications_bakery_id",
                table: "notifications",
                column: "bakery_id");

            migrationBuilder.CreateIndex(
                name: "IX_notifications_customer_id",
                table: "notifications",
                column: "customer_id");

            migrationBuilder.CreateIndex(
                name: "IX_order_detail_available_cake_id",
                table: "order_detail",
                column: "available_cake_id");

            migrationBuilder.CreateIndex(
                name: "IX_order_detail_cake_review_id",
                table: "order_detail",
                column: "cake_review_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_order_detail_custom_cake_id",
                table: "order_detail",
                column: "custom_cake_id");

            migrationBuilder.CreateIndex(
                name: "IX_order_detail_order_id",
                table: "order_detail",
                column: "order_id");

            migrationBuilder.CreateIndex(
                name: "IX_order_supports_bakery_id",
                table: "order_supports",
                column: "bakery_id");

            migrationBuilder.CreateIndex(
                name: "IX_order_supports_customer_id",
                table: "order_supports",
                column: "customer_id");

            migrationBuilder.CreateIndex(
                name: "IX_order_supports_file_id",
                table: "order_supports",
                column: "file_id");

            migrationBuilder.CreateIndex(
                name: "IX_order_supports_order_id",
                table: "order_supports",
                column: "order_id");

            migrationBuilder.CreateIndex(
                name: "IX_orders_bakery_id",
                table: "orders",
                column: "bakery_id");

            migrationBuilder.CreateIndex(
                name: "IX_orders_customer_id",
                table: "orders",
                column: "customer_id");

            migrationBuilder.CreateIndex(
                name: "IX_orders_voucher_id",
                table: "orders",
                column: "voucher_id");

            migrationBuilder.CreateIndex(
                name: "IX_transactions_order_id",
                table: "transactions",
                column: "order_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_vouchers_bakery_id",
                table: "vouchers",
                column: "bakery_id");

            migrationBuilder.CreateIndex(
                name: "IX_wallet_transactions_wallet_id",
                table: "wallet_transactions",
                column: "wallet_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "auths");

            migrationBuilder.DropTable(
                name: "cake_decoration_selections");

            migrationBuilder.DropTable(
                name: "cake_extra_selections");

            migrationBuilder.DropTable(
                name: "cake_message_options");

            migrationBuilder.DropTable(
                name: "cake_part_selections");

            migrationBuilder.DropTable(
                name: "customer_vouchers");

            migrationBuilder.DropTable(
                name: "notifications");

            migrationBuilder.DropTable(
                name: "order_detail");

            migrationBuilder.DropTable(
                name: "order_supports");

            migrationBuilder.DropTable(
                name: "transactions");

            migrationBuilder.DropTable(
                name: "wallet_transactions");

            migrationBuilder.DropTable(
                name: "admins");

            migrationBuilder.DropTable(
                name: "cake_decoration_options");

            migrationBuilder.DropTable(
                name: "cake_extra_options");

            migrationBuilder.DropTable(
                name: "cake_part_options");

            migrationBuilder.DropTable(
                name: "cake_reviews");

            migrationBuilder.DropTable(
                name: "custom_cakes");

            migrationBuilder.DropTable(
                name: "orders");

            migrationBuilder.DropTable(
                name: "wallets");

            migrationBuilder.DropTable(
                name: "available_cakes");

            migrationBuilder.DropTable(
                name: "cake_message_selections");

            migrationBuilder.DropTable(
                name: "customers");

            migrationBuilder.DropTable(
                name: "vouchers");

            migrationBuilder.DropTable(
                name: "bakeries");

            migrationBuilder.DropTable(
                name: "storages");
        }
    }
}

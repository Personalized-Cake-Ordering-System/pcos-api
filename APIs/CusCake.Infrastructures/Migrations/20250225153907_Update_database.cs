using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CusCake.Infrastructures.Migrations
{
    /// <inheritdoc />
    public partial class Update_database : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "confirmed_at",
                table: "bakeries",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "status",
                table: "bakeries",
                type: "longtext",
                nullable: false)
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
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "cake_decorations",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    decoration_name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    decoration_price = table.Column<double>(type: "double", nullable: false),
                    decoration_type = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    decoration_description = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    created_at = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    created_by = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    updated_at = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    updated_by = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    is_deleted = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cake_decorations", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "cake_extras",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    extra_name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    extra_price = table.Column<double>(type: "double", nullable: false),
                    extra_number = table.Column<double>(type: "double", nullable: true),
                    extra_type = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    extra_description = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    created_at = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    created_by = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    updated_at = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    updated_by = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    is_deleted = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cake_extras", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "cake_parts",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    part_name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    part_price = table.Column<double>(type: "double", nullable: false),
                    part_size = table.Column<double>(type: "double", nullable: true),
                    part_type = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    part_description = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    created_at = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    created_by = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    updated_at = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    updated_by = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    is_deleted = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cake_parts", x => x.id);
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
                        name: "FK_custom_cakes_customers_customer_id",
                        column: x => x.customer_id,
                        principalTable: "customers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
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
                    sender_id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    notification_type = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    is_read = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    type = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
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
                    discount_amount = table.Column<double>(type: "double", nullable: false),
                    discount_percentage = table.Column<double>(type: "double", nullable: false),
                    min_order_amount = table.Column<double>(type: "double", nullable: false),
                    max_discount_amount = table.Column<double>(type: "double", nullable: false),
                    expiration_date = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    quantity = table.Column<int>(type: "int", nullable: false),
                    usage_limit = table.Column<int>(type: "int", nullable: false),
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
                name: "cake_decoration_details",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    custom_cake_id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    cake_extra_id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    created_at = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    created_by = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    updated_at = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    updated_by = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    is_deleted = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cake_decoration_details", x => x.id);
                    table.ForeignKey(
                        name: "FK_cake_decoration_details_cake_decorations_cake_extra_id",
                        column: x => x.cake_extra_id,
                        principalTable: "cake_decorations",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_cake_decoration_details_custom_cakes_custom_cake_id",
                        column: x => x.custom_cake_id,
                        principalTable: "custom_cakes",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "cake_extra_details",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    custom_cake_id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    cake_extra_id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    created_at = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    created_by = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    updated_at = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    updated_by = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    is_deleted = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cake_extra_details", x => x.id);
                    table.ForeignKey(
                        name: "FK_cake_extra_details_cake_extras_cake_extra_id",
                        column: x => x.cake_extra_id,
                        principalTable: "cake_extras",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_cake_extra_details_custom_cakes_custom_cake_id",
                        column: x => x.custom_cake_id,
                        principalTable: "custom_cakes",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "cake_messages",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    message_image_id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    message_color = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    message = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    message_type = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    message_description = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    custom_cake_id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    created_at = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    created_by = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    updated_at = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    updated_by = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    is_deleted = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cake_messages", x => x.id);
                    table.ForeignKey(
                        name: "FK_cake_messages_custom_cakes_custom_cake_id",
                        column: x => x.custom_cake_id,
                        principalTable: "custom_cakes",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "cake_part_details",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    custom_cake_id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    cake_part_id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    created_at = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    created_by = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    updated_at = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    updated_by = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    is_deleted = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cake_part_details", x => x.id);
                    table.ForeignKey(
                        name: "FK_cake_part_details_cake_parts_cake_part_id",
                        column: x => x.cake_part_id,
                        principalTable: "cake_parts",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_cake_part_details_custom_cakes_custom_cake_id",
                        column: x => x.custom_cake_id,
                        principalTable: "custom_cakes",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "orders",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    total_price = table.Column<double>(type: "double", nullable: false),
                    order_note = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    pickup_time = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    canceled_reason = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    phone_number = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    order_address = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    order_status = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
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
                        name: "FK_customer_vouchers_vouchers_voucher_id",
                        column: x => x.voucher_id,
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
                    available_cake_id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    cake_note = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    sub_total_price = table.Column<double>(type: "double", nullable: true),
                    cake_review_id = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    custom_cake_id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
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
                    order_support_file_id = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
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
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "transactions",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    amount = table.Column<double>(type: "double", nullable: false),
                    order_id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
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

            migrationBuilder.CreateTable(
                name: "bank_events",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    transaction_id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    gateway = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    transaction_date = table.Column<DateTime>(type: "datetime(6)", nullable: false),
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
                    tranfer_amount = table.Column<double>(type: "double", nullable: false),
                    reference_code = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    accumulated = table.Column<double>(type: "double", nullable: false),
                    is_processed = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    processed_at = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    created_by = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    updated_at = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    updated_by = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    is_deleted = table.Column<bool>(type: "tinyint(1)", nullable: false)
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
                name: "IX_available_cakes_bakery_id",
                table: "available_cakes",
                column: "bakery_id");

            migrationBuilder.CreateIndex(
                name: "IX_bank_events_transaction_id",
                table: "bank_events",
                column: "transaction_id");

            migrationBuilder.CreateIndex(
                name: "IX_cake_decoration_details_cake_extra_id",
                table: "cake_decoration_details",
                column: "cake_extra_id");

            migrationBuilder.CreateIndex(
                name: "IX_cake_decoration_details_custom_cake_id",
                table: "cake_decoration_details",
                column: "custom_cake_id");

            migrationBuilder.CreateIndex(
                name: "IX_cake_extra_details_cake_extra_id",
                table: "cake_extra_details",
                column: "cake_extra_id");

            migrationBuilder.CreateIndex(
                name: "IX_cake_extra_details_custom_cake_id",
                table: "cake_extra_details",
                column: "custom_cake_id");

            migrationBuilder.CreateIndex(
                name: "IX_cake_messages_custom_cake_id",
                table: "cake_messages",
                column: "custom_cake_id");

            migrationBuilder.CreateIndex(
                name: "IX_cake_part_details_cake_part_id",
                table: "cake_part_details",
                column: "cake_part_id");

            migrationBuilder.CreateIndex(
                name: "IX_cake_part_details_custom_cake_id",
                table: "cake_part_details",
                column: "custom_cake_id");

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
                name: "IX_customer_vouchers_customer_id",
                table: "customer_vouchers",
                column: "customer_id");

            migrationBuilder.CreateIndex(
                name: "IX_customer_vouchers_oder_id",
                table: "customer_vouchers",
                column: "oder_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_customer_vouchers_voucher_id",
                table: "customer_vouchers",
                column: "voucher_id");

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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "bank_events");

            migrationBuilder.DropTable(
                name: "cake_decoration_details");

            migrationBuilder.DropTable(
                name: "cake_extra_details");

            migrationBuilder.DropTable(
                name: "cake_messages");

            migrationBuilder.DropTable(
                name: "cake_part_details");

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
                name: "cake_decorations");

            migrationBuilder.DropTable(
                name: "cake_extras");

            migrationBuilder.DropTable(
                name: "cake_parts");

            migrationBuilder.DropTable(
                name: "cake_reviews");

            migrationBuilder.DropTable(
                name: "custom_cakes");

            migrationBuilder.DropTable(
                name: "orders");

            migrationBuilder.DropTable(
                name: "available_cakes");

            migrationBuilder.DropTable(
                name: "vouchers");

            migrationBuilder.DropColumn(
                name: "confirmed_at",
                table: "bakeries");

            migrationBuilder.DropColumn(
                name: "status",
                table: "bakeries");
        }
    }
}

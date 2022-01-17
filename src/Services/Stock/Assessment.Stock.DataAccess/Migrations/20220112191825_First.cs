using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Assessment.Stock.DataAccess.Migrations
{
    public partial class First : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "public");

            migrationBuilder.CreateTable(
                name: "product",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    stock = table.Column<int>(type: "integer", nullable: false),
                    price = table.Column<decimal>(type: "numeric", nullable: false),
                    deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_product", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "user",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    surname = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    email = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    username = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    password = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    avatar = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: true),
                    deleted = table.Column<bool>(type: "boolean", nullable: false),
                    package_end_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    api_code = table.Column<string>(type: "character varying(400)", maxLength: 400, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "invoice",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    product_id = table.Column<int>(type: "integer", nullable: false),
                    customer_name = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    customer_surname = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    amount = table.Column<decimal>(type: "numeric", nullable: false),
                    invoice_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_invoice", x => x.id);
                    table.ForeignKey(
                        name: "FK_invoice_product_product_id",
                        column: x => x.product_id,
                        principalSchema: "public",
                        principalTable: "product",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "order",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    store_id = table.Column<int>(type: "integer", nullable: false),
                    product_id = table.Column<int>(type: "integer", nullable: false),
                    order_product_count = table.Column<int>(type: "integer", nullable: false),
                    amount = table.Column<decimal>(type: "numeric", nullable: false),
                    invoice_id = table.Column<int>(type: "integer", nullable: true),
                    invoice_status = table.Column<int>(type: "integer", nullable: false),
                    order_status = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    order_time = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_order", x => x.id);
                    table.ForeignKey(
                        name: "FK_order_invoice_invoice_id",
                        column: x => x.invoice_id,
                        principalSchema: "public",
                        principalTable: "invoice",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_order_product_product_id",
                        column: x => x.product_id,
                        principalSchema: "public",
                        principalTable: "product",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                schema: "public",
                table: "product",
                columns: new[] { "id", "deleted", "name", "price", "stock" },
                values: new object[] { 1, false, "IPhone 15", 15000m, 15 });

            migrationBuilder.InsertData(
                schema: "public",
                table: "user",
                columns: new[] { "id", "api_code", "avatar", "deleted", "email", "name", "package_end_date", "password", "surname", "username" },
                values: new object[] { 1, null, "https://pngset.com/images/image-library-stock-boy-svg-kid-child-avatar-icon-clothing-snowman-indoors-face-transparent-png-442997.png", false, "info@stock.com", "Stock", null, "123456", "Stock", "info@stock.com" });

            migrationBuilder.InsertData(
                schema: "public",
                table: "order",
                columns: new[] { "id", "amount", "invoice_id", "invoice_status", "order_product_count", "order_status", "order_time", "product_id", "store_id" },
                values: new object[] { 1, 30000m, null, 0, 2, "Completed", new DateTime(2022, 1, 12, 22, 18, 24, 594, DateTimeKind.Local).AddTicks(8506), 1, 37814 });

            migrationBuilder.CreateIndex(
                name: "IX_invoice_id",
                schema: "public",
                table: "invoice",
                column: "id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_invoice_product_id",
                schema: "public",
                table: "invoice",
                column: "product_id");

            migrationBuilder.CreateIndex(
                name: "IX_order_id",
                schema: "public",
                table: "order",
                column: "id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_order_invoice_id",
                schema: "public",
                table: "order",
                column: "invoice_id");

            migrationBuilder.CreateIndex(
                name: "IX_order_product_id",
                schema: "public",
                table: "order",
                column: "product_id");

            migrationBuilder.CreateIndex(
                name: "IX_order_store_id_product_id_invoice_id",
                schema: "public",
                table: "order",
                columns: new[] { "store_id", "product_id", "invoice_id" });

            migrationBuilder.CreateIndex(
                name: "IX_product_id",
                schema: "public",
                table: "product",
                column: "id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_user_id_username",
                schema: "public",
                table: "user",
                columns: new[] { "id", "username" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "order",
                schema: "public");

            migrationBuilder.DropTable(
                name: "user",
                schema: "public");

            migrationBuilder.DropTable(
                name: "invoice",
                schema: "public");

            migrationBuilder.DropTable(
                name: "product",
                schema: "public");
        }
    }
}

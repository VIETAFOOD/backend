using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Presentation.Migrations
{
    public partial class tiensql : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Admin",
                columns: table => new
                {
                    adminKey = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    fullName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    email = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    password = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Admin__586B4031D6DBE445", x => x.adminKey);
                });

            migrationBuilder.CreateTable(
                name: "Coupon",
                columns: table => new
                {
                    couponKey = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    couponName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    discountPercentage = table.Column<double>(type: "float", nullable: false),
                    numOfUses = table.Column<int>(type: "int", nullable: false),
                    expiredDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    createdBy = table.Column<int>(type: "int", nullable: false),
                    createdDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    status = table.Column<byte>(type: "tinyint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Coupon__592794AC4F385980", x => x.couponKey);
                });

            migrationBuilder.CreateTable(
                name: "CustomerInformation",
                columns: table => new
                {
                    customerInfoKey = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    email = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    address = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    phone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Customer__B949C15F4A84291B", x => x.customerInfoKey);
                });

            migrationBuilder.CreateTable(
                name: "Product",
                columns: table => new
                {
                    productKey = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    guildToUsing = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    weight = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    expiryDay = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    price = table.Column<double>(type: "float", nullable: false),
                    imageURL = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    quantity = table.Column<int>(type: "int", nullable: false),
                    status = table.Column<byte>(type: "tinyint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Product__1E79644AE3B1B19E", x => x.productKey);
                });

            migrationBuilder.CreateTable(
                name: "Order",
                columns: table => new
                {
                    orderKey = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    customerInfoKey = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    couponKey = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    createdAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    orderStatus = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    totalPrice = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    status = table.Column<byte>(type: "tinyint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Order__594FCFFBF63B0FBA", x => x.orderKey);
                    table.ForeignKey(
                        name: "FK__Order__couponKey__52593CB8",
                        column: x => x.couponKey,
                        principalTable: "Coupon",
                        principalColumn: "couponKey");
                    table.ForeignKey(
                        name: "FK__Order__customerI__5165187F",
                        column: x => x.customerInfoKey,
                        principalTable: "CustomerInformation",
                        principalColumn: "customerInfoKey");
                });

            migrationBuilder.CreateTable(
                name: "OrderDetail",
                columns: table => new
                {
                    orderDetailKey = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    productKey = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    orderKey = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    quantity = table.Column<int>(type: "int", nullable: true),
                    actualPrice = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__OrderDet__34730B9072589CF9", x => x.orderDetailKey);
                    table.ForeignKey(
                        name: "FK__OrderDeta__order__5629CD9C",
                        column: x => x.orderKey,
                        principalTable: "Order",
                        principalColumn: "orderKey");
                    table.ForeignKey(
                        name: "FK__OrderDeta__produ__5535A963",
                        column: x => x.productKey,
                        principalTable: "Product",
                        principalColumn: "productKey");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Order_couponKey",
                table: "Order",
                column: "couponKey");

            migrationBuilder.CreateIndex(
                name: "IX_Order_customerInfoKey",
                table: "Order",
                column: "customerInfoKey");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetail_orderKey",
                table: "OrderDetail",
                column: "orderKey");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetail_productKey",
                table: "OrderDetail",
                column: "productKey");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Admin");

            migrationBuilder.DropTable(
                name: "OrderDetail");

            migrationBuilder.DropTable(
                name: "Order");

            migrationBuilder.DropTable(
                name: "Product");

            migrationBuilder.DropTable(
                name: "Coupon");

            migrationBuilder.DropTable(
                name: "CustomerInformation");
        }
    }
}

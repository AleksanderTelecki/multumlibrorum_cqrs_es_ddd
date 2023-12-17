using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sales.Domain.Migrations
{
    /// <inheritdoc />
    public partial class orderinit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartItem_Cart_CartId",
                schema: "Sales",
                table: "CartItem");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CartItem",
                schema: "Sales",
                table: "CartItem");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Cart",
                schema: "Sales",
                table: "Cart");

            migrationBuilder.EnsureSchema(
                name: "Sale");

            migrationBuilder.RenameTable(
                name: "CartItem",
                schema: "Sales",
                newName: "CartItems",
                newSchema: "Sale");

            migrationBuilder.RenameTable(
                name: "Cart",
                schema: "Sales",
                newName: "Carts",
                newSchema: "Sale");

            migrationBuilder.RenameIndex(
                name: "IX_CartItem_CartId",
                schema: "Sale",
                table: "CartItems",
                newName: "IX_CartItems_CartId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CartItems",
                schema: "Sale",
                table: "CartItems",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Carts",
                schema: "Sale",
                table: "Carts",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Orders",
                schema: "Sale",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClientId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    State = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OrderItems",
                schema: "Sale",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    OrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderItems_Orders_OrderId",
                        column: x => x.OrderId,
                        principalSchema: "Sale",
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_OrderId",
                schema: "Sale",
                table: "OrderItems",
                column: "OrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_CartItems_Carts_CartId",
                schema: "Sale",
                table: "CartItems",
                column: "CartId",
                principalSchema: "Sale",
                principalTable: "Carts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartItems_Carts_CartId",
                schema: "Sale",
                table: "CartItems");

            migrationBuilder.DropTable(
                name: "OrderItems",
                schema: "Sale");

            migrationBuilder.DropTable(
                name: "Orders",
                schema: "Sale");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Carts",
                schema: "Sale",
                table: "Carts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CartItems",
                schema: "Sale",
                table: "CartItems");

            migrationBuilder.EnsureSchema(
                name: "Sales");

            migrationBuilder.RenameTable(
                name: "Carts",
                schema: "Sale",
                newName: "Cart",
                newSchema: "Sales");

            migrationBuilder.RenameTable(
                name: "CartItems",
                schema: "Sale",
                newName: "CartItem",
                newSchema: "Sales");

            migrationBuilder.RenameIndex(
                name: "IX_CartItems_CartId",
                schema: "Sales",
                table: "CartItem",
                newName: "IX_CartItem_CartId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Cart",
                schema: "Sales",
                table: "Cart",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CartItem",
                schema: "Sales",
                table: "CartItem",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CartItem_Cart_CartId",
                schema: "Sales",
                table: "CartItem",
                column: "CartId",
                principalSchema: "Sales",
                principalTable: "Cart",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

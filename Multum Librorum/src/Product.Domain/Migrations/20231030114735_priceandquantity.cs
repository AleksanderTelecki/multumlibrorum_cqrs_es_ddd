﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Product.Domain.Migrations
{
    /// <inheritdoc />
    public partial class priceandquantity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                schema: "Product",
                table: "Books",
                type: "decimal(6,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                schema: "Product",
                table: "Books",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Price",
                schema: "Product",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "Quantity",
                schema: "Product",
                table: "Books");
        }
    }
}

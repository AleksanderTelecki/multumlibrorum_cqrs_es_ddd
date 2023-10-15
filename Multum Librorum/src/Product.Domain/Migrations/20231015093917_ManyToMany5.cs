using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Product.Domain.Migrations
{
    /// <inheritdoc />
    public partial class ManyToMany5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_BookPromotions",
                schema: "Product",
                table: "BookPromotions");

            migrationBuilder.DropIndex(
                name: "IX_BookPromotions_BookId",
                schema: "Product",
                table: "BookPromotions");

            migrationBuilder.DropColumn(
                name: "Id",
                schema: "Product",
                table: "BookPromotions");

            migrationBuilder.DropColumn(
                name: "BookId1",
                schema: "Product",
                table: "BookPromotions");

            migrationBuilder.DropColumn(
                name: "PromotionId1",
                schema: "Product",
                table: "BookPromotions");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BookPromotions",
                schema: "Product",
                table: "BookPromotions",
                columns: new[] { "BookId", "PromotionId" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_BookPromotions",
                schema: "Product",
                table: "BookPromotions");

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                schema: "Product",
                table: "BookPromotions",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "BookId1",
                schema: "Product",
                table: "BookPromotions",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "PromotionId1",
                schema: "Product",
                table: "BookPromotions",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_BookPromotions",
                schema: "Product",
                table: "BookPromotions",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_BookPromotions_BookId",
                schema: "Product",
                table: "BookPromotions",
                column: "BookId");
        }
    }
}

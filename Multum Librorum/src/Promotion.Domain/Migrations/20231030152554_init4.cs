using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Promotion.Domain.Migrations
{
    /// <inheritdoc />
    public partial class init4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PromotionProduct_Promotions_PromotionId",
                schema: "Promotion",
                table: "PromotionProduct");

            migrationBuilder.AlterColumn<Guid>(
                name: "PromotionId",
                schema: "Promotion",
                table: "PromotionProduct",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_PromotionProduct_Promotions_PromotionId",
                schema: "Promotion",
                table: "PromotionProduct",
                column: "PromotionId",
                principalSchema: "Promotion",
                principalTable: "Promotions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PromotionProduct_Promotions_PromotionId",
                schema: "Promotion",
                table: "PromotionProduct");

            migrationBuilder.AlterColumn<Guid>(
                name: "PromotionId",
                schema: "Promotion",
                table: "PromotionProduct",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddForeignKey(
                name: "FK_PromotionProduct_Promotions_PromotionId",
                schema: "Promotion",
                table: "PromotionProduct",
                column: "PromotionId",
                principalSchema: "Promotion",
                principalTable: "Promotions",
                principalColumn: "Id");
        }
    }
}

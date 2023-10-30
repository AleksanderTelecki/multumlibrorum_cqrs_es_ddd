using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Promotion.Domain.Migrations
{
    /// <inheritdoc />
    public partial class init5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Promotions_Promotions_PromotionEntityId",
                schema: "Promotion",
                table: "Promotions");

            migrationBuilder.DropIndex(
                name: "IX_Promotions_PromotionEntityId",
                schema: "Promotion",
                table: "Promotions");

            migrationBuilder.DropColumn(
                name: "PromotionEntityId",
                schema: "Promotion",
                table: "Promotions");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "PromotionEntityId",
                schema: "Promotion",
                table: "Promotions",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Promotions_PromotionEntityId",
                schema: "Promotion",
                table: "Promotions",
                column: "PromotionEntityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Promotions_Promotions_PromotionEntityId",
                schema: "Promotion",
                table: "Promotions",
                column: "PromotionEntityId",
                principalSchema: "Promotion",
                principalTable: "Promotions",
                principalColumn: "Id");
        }
    }
}

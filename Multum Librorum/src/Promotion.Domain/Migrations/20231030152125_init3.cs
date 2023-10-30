using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Promotion.Domain.Migrations
{
    /// <inheritdoc />
    public partial class init3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Promotions_Promotions_PromotionId",
                schema: "PromotionProduct",
                table: "Promotions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Promotions",
                schema: "PromotionProduct",
                table: "Promotions");

            migrationBuilder.RenameTable(
                name: "Promotions",
                schema: "PromotionProduct",
                newName: "PromotionProduct",
                newSchema: "Promotion");

            migrationBuilder.RenameIndex(
                name: "IX_Promotions_PromotionId",
                schema: "Promotion",
                table: "PromotionProduct",
                newName: "IX_PromotionProduct_PromotionId");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                schema: "Promotion",
                table: "Promotions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_PromotionProduct",
                schema: "Promotion",
                table: "PromotionProduct",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PromotionProduct_Promotions_PromotionId",
                schema: "Promotion",
                table: "PromotionProduct",
                column: "PromotionId",
                principalSchema: "Promotion",
                principalTable: "Promotions",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PromotionProduct_Promotions_PromotionId",
                schema: "Promotion",
                table: "PromotionProduct");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PromotionProduct",
                schema: "Promotion",
                table: "PromotionProduct");

            migrationBuilder.DropColumn(
                name: "Description",
                schema: "Promotion",
                table: "Promotions");

            migrationBuilder.EnsureSchema(
                name: "PromotionProduct");

            migrationBuilder.RenameTable(
                name: "PromotionProduct",
                schema: "Promotion",
                newName: "Promotions",
                newSchema: "PromotionProduct");

            migrationBuilder.RenameIndex(
                name: "IX_PromotionProduct_PromotionId",
                schema: "PromotionProduct",
                table: "Promotions",
                newName: "IX_Promotions_PromotionId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Promotions",
                schema: "PromotionProduct",
                table: "Promotions",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Promotions_Promotions_PromotionId",
                schema: "PromotionProduct",
                table: "Promotions",
                column: "PromotionId",
                principalSchema: "Promotion",
                principalTable: "Promotions",
                principalColumn: "Id");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Product.Domain.Migrations
{
    /// <inheritdoc />
    public partial class ManyToMany4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookPromotions_Books_PromotedBooksId",
                schema: "Product",
                table: "BookPromotions");

            migrationBuilder.DropForeignKey(
                name: "FK_BookPromotions_Promotions_PromotionsId",
                schema: "Product",
                table: "BookPromotions");

            migrationBuilder.DropIndex(
                name: "IX_BookPromotions_PromotedBooksId",
                schema: "Product",
                table: "BookPromotions");

            migrationBuilder.DropIndex(
                name: "IX_BookPromotions_PromotionsId",
                schema: "Product",
                table: "BookPromotions");

            migrationBuilder.RenameColumn(
                name: "PromotionsId",
                schema: "Product",
                table: "BookPromotions",
                newName: "PromotionId1");

            migrationBuilder.RenameColumn(
                name: "PromotedBooksId",
                schema: "Product",
                table: "BookPromotions",
                newName: "BookId1");

            migrationBuilder.CreateIndex(
                name: "IX_BookPromotions_BookId",
                schema: "Product",
                table: "BookPromotions",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_BookPromotions_PromotionId",
                schema: "Product",
                table: "BookPromotions",
                column: "PromotionId");

            migrationBuilder.AddForeignKey(
                name: "FK_BookPromotions_Books_BookId",
                schema: "Product",
                table: "BookPromotions",
                column: "BookId",
                principalSchema: "Product",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BookPromotions_Promotions_PromotionId",
                schema: "Product",
                table: "BookPromotions",
                column: "PromotionId",
                principalSchema: "Product",
                principalTable: "Promotions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookPromotions_Books_BookId",
                schema: "Product",
                table: "BookPromotions");

            migrationBuilder.DropForeignKey(
                name: "FK_BookPromotions_Promotions_PromotionId",
                schema: "Product",
                table: "BookPromotions");

            migrationBuilder.DropIndex(
                name: "IX_BookPromotions_BookId",
                schema: "Product",
                table: "BookPromotions");

            migrationBuilder.DropIndex(
                name: "IX_BookPromotions_PromotionId",
                schema: "Product",
                table: "BookPromotions");

            migrationBuilder.RenameColumn(
                name: "PromotionId1",
                schema: "Product",
                table: "BookPromotions",
                newName: "PromotionsId");

            migrationBuilder.RenameColumn(
                name: "BookId1",
                schema: "Product",
                table: "BookPromotions",
                newName: "PromotedBooksId");

            migrationBuilder.CreateIndex(
                name: "IX_BookPromotions_PromotedBooksId",
                schema: "Product",
                table: "BookPromotions",
                column: "PromotedBooksId");

            migrationBuilder.CreateIndex(
                name: "IX_BookPromotions_PromotionsId",
                schema: "Product",
                table: "BookPromotions",
                column: "PromotionsId");

            migrationBuilder.AddForeignKey(
                name: "FK_BookPromotions_Books_PromotedBooksId",
                schema: "Product",
                table: "BookPromotions",
                column: "PromotedBooksId",
                principalSchema: "Product",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BookPromotions_Promotions_PromotionsId",
                schema: "Product",
                table: "BookPromotions",
                column: "PromotionsId",
                principalSchema: "Product",
                principalTable: "Promotions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

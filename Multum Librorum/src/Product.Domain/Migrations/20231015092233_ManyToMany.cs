using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Product.Domain.Migrations
{
    /// <inheritdoc />
    public partial class ManyToMany : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_Promotions_PromotionEntityId",
                schema: "Product",
                table: "Books");

            migrationBuilder.DropIndex(
                name: "IX_Books_PromotionEntityId",
                schema: "Product",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "PromotionEntityId",
                schema: "Product",
                table: "Books");

            migrationBuilder.CreateTable(
                name: "BookEntityPromotionEntity",
                schema: "Product",
                columns: table => new
                {
                    PromotedBooksId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PromotionsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookEntityPromotionEntity", x => new { x.PromotedBooksId, x.PromotionsId });
                    table.ForeignKey(
                        name: "FK_BookEntityPromotionEntity_Books_PromotedBooksId",
                        column: x => x.PromotedBooksId,
                        principalSchema: "Product",
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BookEntityPromotionEntity_Promotions_PromotionsId",
                        column: x => x.PromotionsId,
                        principalSchema: "Product",
                        principalTable: "Promotions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BookEntityPromotionEntity_PromotionsId",
                schema: "Product",
                table: "BookEntityPromotionEntity",
                column: "PromotionsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BookEntityPromotionEntity",
                schema: "Product");

            migrationBuilder.AddColumn<Guid>(
                name: "PromotionEntityId",
                schema: "Product",
                table: "Books",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Books_PromotionEntityId",
                schema: "Product",
                table: "Books",
                column: "PromotionEntityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Promotions_PromotionEntityId",
                schema: "Product",
                table: "Books",
                column: "PromotionEntityId",
                principalSchema: "Product",
                principalTable: "Promotions",
                principalColumn: "Id");
        }
    }
}

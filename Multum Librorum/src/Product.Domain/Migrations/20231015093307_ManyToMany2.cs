using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Product.Domain.Migrations
{
    /// <inheritdoc />
    public partial class ManyToMany2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BookEntityPromotionEntity",
                schema: "Product");

            migrationBuilder.CreateTable(
                name: "BookPromotions",
                schema: "Product",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PromotionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BookId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PromotedBooksId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PromotionsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookPromotions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BookPromotions_Books_PromotedBooksId",
                        column: x => x.PromotedBooksId,
                        principalSchema: "Product",
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BookPromotions_Promotions_PromotionsId",
                        column: x => x.PromotionsId,
                        principalSchema: "Product",
                        principalTable: "Promotions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BookPromotions",
                schema: "Product");

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
    }
}

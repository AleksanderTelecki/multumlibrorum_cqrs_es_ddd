using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Promotion.Domain.Migrations
{
    /// <inheritdoc />
    public partial class init2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "PromotionProduct");

            migrationBuilder.CreateTable(
                name: "Promotions",
                schema: "PromotionProduct",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PromotionId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Promotions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Promotions_Promotions_PromotionId",
                        column: x => x.PromotionId,
                        principalSchema: "Promotion",
                        principalTable: "Promotions",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Promotions_PromotionId",
                schema: "PromotionProduct",
                table: "Promotions",
                column: "PromotionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Promotions",
                schema: "PromotionProduct");
        }
    }
}

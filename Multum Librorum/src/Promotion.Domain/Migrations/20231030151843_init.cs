using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Promotion.Domain.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Promotion");

            migrationBuilder.CreateTable(
                name: "Promotions",
                schema: "Promotion",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PromotionInPercentage = table.Column<decimal>(type: "decimal(6,2)", nullable: false),
                    Regdate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PromotionEntityId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Promotions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Promotions_Promotions_PromotionEntityId",
                        column: x => x.PromotionEntityId,
                        principalSchema: "Promotion",
                        principalTable: "Promotions",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Promotions_PromotionEntityId",
                schema: "Promotion",
                table: "Promotions",
                column: "PromotionEntityId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Promotions",
                schema: "Promotion");
        }
    }
}

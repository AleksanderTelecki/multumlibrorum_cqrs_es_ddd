using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Product.Domain.Migrations
{
    /// <inheritdoc />
    public partial class promotedprice : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "PromotedPrice",
                schema: "Product",
                table: "Books",
                type: "decimal(6,2)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PromotedPrice",
                schema: "Product",
                table: "Books");
        }
    }
}

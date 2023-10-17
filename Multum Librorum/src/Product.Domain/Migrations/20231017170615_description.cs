using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Product.Domain.Migrations
{
    /// <inheritdoc />
    public partial class description : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Price",
                schema: "Product",
                table: "Books");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                schema: "Product",
                table: "Books",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                schema: "Product",
                table: "Books");

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                schema: "Product",
                table: "Books",
                type: "decimal(6,2)",
                nullable: false,
                defaultValue: 0m);
        }
    }
}

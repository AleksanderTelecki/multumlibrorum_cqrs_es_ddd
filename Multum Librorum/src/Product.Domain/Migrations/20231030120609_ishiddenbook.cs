using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Product.Domain.Migrations
{
    /// <inheritdoc />
    public partial class ishiddenbook : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsHidden",
                schema: "Product",
                table: "Books",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsHidden",
                schema: "Product",
                table: "Books");
        }
    }
}

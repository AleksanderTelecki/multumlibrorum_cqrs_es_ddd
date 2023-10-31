using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Promotion.Domain.Migrations
{
    /// <inheritdoc />
    public partial class isactiveprom : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                schema: "Promotion",
                table: "Promotions",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                schema: "Promotion",
                table: "Promotions");
        }
    }
}

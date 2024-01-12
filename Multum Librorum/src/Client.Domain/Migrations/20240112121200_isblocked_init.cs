using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Client.Domain.Migrations
{
    /// <inheritdoc />
    public partial class isblocked_init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsBlocked",
                schema: "Client",
                table: "Clients",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsBlocked",
                schema: "Client",
                table: "Clients");
        }
    }
}

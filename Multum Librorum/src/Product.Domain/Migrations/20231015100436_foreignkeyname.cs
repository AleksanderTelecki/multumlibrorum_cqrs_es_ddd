using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Product.Domain.Migrations
{
    /// <inheritdoc />
    public partial class foreignkeyname : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Books_BookEntityId",
                schema: "Product",
                table: "Comments");

            migrationBuilder.RenameColumn(
                name: "BookEntityId",
                schema: "Product",
                table: "Comments",
                newName: "BookId");

            migrationBuilder.RenameIndex(
                name: "IX_Comments_BookEntityId",
                schema: "Product",
                table: "Comments",
                newName: "IX_Comments_BookId");

            migrationBuilder.AlterColumn<Guid>(
                name: "BookId",
                schema: "Product",
                table: "Comments",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Books_BookId",
                schema: "Product",
                table: "Comments",
                column: "BookId",
                principalSchema: "Product",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Books_BookId",
                schema: "Product",
                table: "Comments");

            migrationBuilder.RenameColumn(
                name: "BookId",
                schema: "Product",
                table: "Comments",
                newName: "BookEntityId");

            migrationBuilder.RenameIndex(
                name: "IX_Comments_BookId",
                schema: "Product",
                table: "Comments",
                newName: "IX_Comments_BookEntityId");

            migrationBuilder.AlterColumn<Guid>(
                name: "BookEntityId",
                schema: "Product",
                table: "Comments",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Books_BookEntityId",
                schema: "Product",
                table: "Comments",
                column: "BookEntityId",
                principalSchema: "Product",
                principalTable: "Books",
                principalColumn: "Id");
        }
    }
}

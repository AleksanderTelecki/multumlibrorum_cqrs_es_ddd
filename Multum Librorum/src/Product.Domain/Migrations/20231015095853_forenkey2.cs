﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Product.Domain.Migrations
{
    /// <inheritdoc />
    public partial class forenkey2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Books_BookEntityId",
                schema: "Product",
                table: "Comments");

            migrationBuilder.AlterColumn<Guid>(
                name: "BookEntityId",
                schema: "Product",
                table: "Comments",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<Guid>(
                name: "BookId",
                schema: "Product",
                table: "Comments",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Books_BookEntityId",
                schema: "Product",
                table: "Comments",
                column: "BookEntityId",
                principalSchema: "Product",
                principalTable: "Books",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Books_BookEntityId",
                schema: "Product",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "BookId",
                schema: "Product",
                table: "Comments");

            migrationBuilder.AlterColumn<Guid>(
                name: "BookEntityId",
                schema: "Product",
                table: "Comments",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Books_BookEntityId",
                schema: "Product",
                table: "Comments",
                column: "BookEntityId",
                principalSchema: "Product",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

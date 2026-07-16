using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Arzly.Api.Migrations
{
    /// <inheritdoc />
    public partial class added_litlingTitle_personName_chat : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ListingTitle",
                table: "Chats",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PersonName",
                table: "Chats",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"),
                column: "ConcurrencyStamp",
                value: "82ecc78a-642f-4069-b8c0-9c9cfcd1504d");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000002"),
                column: "ConcurrencyStamp",
                value: "f052f54d-73a7-4357-b00e-193eb565d4c6");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000003"),
                column: "ConcurrencyStamp",
                value: "f0c0a8c4-5410-4eed-b238-190907cf6dfe");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ListingTitle",
                table: "Chats");

            migrationBuilder.DropColumn(
                name: "PersonName",
                table: "Chats");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"),
                column: "ConcurrencyStamp",
                value: "d883a22f-7b4c-4b2e-b823-82ffeea1b2d0");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000002"),
                column: "ConcurrencyStamp",
                value: "a202ca5d-5a69-4304-bcc3-5c962f7d044b");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000003"),
                column: "ConcurrencyStamp",
                value: "dfa65e54-ef10-4f83-bacf-5b4169788cc2");
        }
    }
}

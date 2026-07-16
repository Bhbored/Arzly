using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Arzly.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddIsDiscontinuedIndexToChat : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"),
                column: "ConcurrencyStamp",
                value: "d3f6835c-ab84-4f92-b19d-3622ba8c431c");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000002"),
                column: "ConcurrencyStamp",
                value: "24a7d37d-3cde-4310-9bf8-62eae5368257");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000003"),
                column: "ConcurrencyStamp",
                value: "36887550-f3dd-4d71-a594-cfda2bc49de2");

            migrationBuilder.CreateIndex(
                name: "IX_Chats_IsDiscontinued",
                table: "Chats",
                column: "IsDiscontinued");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Chats_IsDiscontinued",
                table: "Chats");

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
    }
}

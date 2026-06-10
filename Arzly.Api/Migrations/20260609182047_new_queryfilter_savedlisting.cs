using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Arzly.Api.Migrations
{
    /// <inheritdoc />
    public partial class new_queryfilter_savedlisting : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"),
                column: "ConcurrencyStamp",
                value: "0796b43b-f911-4181-85ea-fe14fc87a85d");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000002"),
                column: "ConcurrencyStamp",
                value: "9ace4d2f-317b-4470-a9f8-6926fd65e707");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000003"),
                column: "ConcurrencyStamp",
                value: "b2359009-c6b5-48c8-a267-8aa8503532e2");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"),
                column: "ConcurrencyStamp",
                value: "c332c8f3-648b-4201-9384-acaabdeb45f3");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000002"),
                column: "ConcurrencyStamp",
                value: "da2216d6-5b4f-4fdd-8e7a-35f1a3cb4252");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000003"),
                column: "ConcurrencyStamp",
                value: "8372e0e3-c74b-4947-86e9-87fdee38a88d");
        }
    }
}

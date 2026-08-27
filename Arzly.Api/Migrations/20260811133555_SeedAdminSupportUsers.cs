using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Arzly.Api.Migrations
{
    /// <inheritdoc />
    public partial class SeedAdminSupportUsers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"),
                column: "ConcurrencyStamp",
                value: "5ba3ae6f-8015-4213-bd5c-482c6e684d66");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000002"),
                column: "ConcurrencyStamp",
                value: "e485bfa3-254f-47ad-81de-225789f04724");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000003"),
                column: "ConcurrencyStamp",
                value: "fe14f4dd-7cc7-4192-be64-003fe999513e");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "AuthMethod", "BanExpiresAt", "BanReason", "ConcurrencyStamp", "CreatedAt", "DeletedAt", "Email", "EmailConfirmed", "FirebaseUid", "IsBanned", "IsDeleted", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "RefreshToken", "RefreshTokenExpirateDate", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { new Guid("00000000-0000-0000-0000-00000000000a"), 0, 1, null, null, "13bf12aa-1c6f-45c7-9a97-390992d8f06e", new DateTime(2026, 8, 11, 13, 35, 53, 933, DateTimeKind.Utc).AddTicks(5696), null, "bourhan-admin@gmail.com", true, null, false, false, false, null, "BOURHAN-ADMIN@GMAIL.COM", "BOURHAN-ADMIN@GMAIL.COM", "AQAAAAIAAYagAAAAEAN/OwCjwTSTKWuGvMdIbh4RP/Pb6CxIXOJ6pJOSbrRU9I9dy9UIHeWFB7N2G0yqow==", null, false, null, null, "ADMIN-SEED-STAMP", false, "bourhan-admin@gmail.com" },
                    { new Guid("00000000-0000-0000-0000-00000000000b"), 0, 1, null, null, "d36dc50e-1d76-4990-ae5c-969308e1c7e6", new DateTime(2026, 8, 11, 13, 35, 53, 933, DateTimeKind.Utc).AddTicks(5954), null, "bourhan-support@gmail.com", true, null, false, false, false, null, "BOURHAN-SUPPORT@GMAIL.COM", "BOURHAN-SUPPORT@GMAIL.COM", "AQAAAAIAAYagAAAAEJqqzmJDfcitN17c37+dIhknC8cUFSKBicxS7Wc/mfMugQZw3qJVIoRcLAcOtR0RNA==", null, false, null, null, "SUPPORT-SEED-STAMP", false, "bourhan-support@gmail.com" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { new Guid("00000000-0000-0000-0000-000000000001"), new Guid("00000000-0000-0000-0000-00000000000a") },
                    { new Guid("00000000-0000-0000-0000-000000000002"), new Guid("00000000-0000-0000-0000-00000000000b") }
                });

            migrationBuilder.InsertData(
                table: "UserProfiles",
                columns: new[] { "UserId", "Email", "FullName", "IsStore", "IsVerified", "LastActiveAt", "PhoneNumber", "ProfileImageUrl", "PublicLocation", "PublicName", "StoreDescription", "UpdateddAt" },
                values: new object[,]
                {
                    { new Guid("00000000-0000-0000-0000-00000000000a"), "bourhan-admin@gmail.com", "Arzly Admin", false, false, null, null, null, null, null, null, null },
                    { new Guid("00000000-0000-0000-0000-00000000000b"), "bourhan-support@gmail.com", "Arzly Support", false, false, null, null, null, null, null, null, null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("00000000-0000-0000-0000-000000000001"), new Guid("00000000-0000-0000-0000-00000000000a") });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("00000000-0000-0000-0000-000000000002"), new Guid("00000000-0000-0000-0000-00000000000b") });

            migrationBuilder.DeleteData(
                table: "UserProfiles",
                keyColumn: "UserId",
                keyValue: new Guid("00000000-0000-0000-0000-00000000000a"));

            migrationBuilder.DeleteData(
                table: "UserProfiles",
                keyColumn: "UserId",
                keyValue: new Guid("00000000-0000-0000-0000-00000000000b"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-00000000000a"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-00000000000b"));

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"),
                column: "ConcurrencyStamp",
                value: "0aba6b18-43e6-4665-ae61-afc83b2b3bcb");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000002"),
                column: "ConcurrencyStamp",
                value: "98fbe67b-e133-4415-a40c-eb6c41e0b845");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000003"),
                column: "ConcurrencyStamp",
                value: "20238496-a66e-4f3a-b42e-4cb3bd9e3c74");
        }
    }
}

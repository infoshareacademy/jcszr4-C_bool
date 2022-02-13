using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace C_bool.BLL.Migrations
{
    public partial class SeedRolesAndSuperAdminUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { 1, "b767c764-d9b6-4128-9f38-70e65aee8554", "Admin", "ADMIN" },
                    { 2, "33d02d09-c152-42c2-bbb3-2496f30d9778", "Moderator", "MODERATOR" },
                    { 3, "341050d8-4c22-4632-aebc-0b1e8b315767", "User", "USER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "CreatedOn", "Email", "EmailConfirmed", "Gender", "IsActive", "Latitude", "LockoutEnabled", "LockoutEnd", "Longitude", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "Photo", "Points", "SecurityStamp", "TwoFactorEnabled", "UserBadges", "UserName" },
                values: new object[] { 1, 0, "4203212b-99fc-4da0-9dcd-db25941acebb", new DateTime(2022, 2, 13, 1, 53, 58, 122, DateTimeKind.Utc).AddTicks(9947), "super@admin.com", true, 0, true, 0.0, false, null, 0.0, "SUPER@ADMIN.COM", "SUPERADMIN", "AQAAAAEAACcQAAAAEHL1M4LqdpudxQtaf4Fd0lAmd0QpbHosvOPlAGmUzv+KRa5YQKVsA9iVpeQGiVADmQ==", null, false, null, 0, "abd1e4cc-f25d-47f1-a5f0-c8e7ee263db8", false, null, "SuperAdmin" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { 1, 1 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}

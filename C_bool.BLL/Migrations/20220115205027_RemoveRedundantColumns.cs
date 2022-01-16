using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace C_bool.BLL.Migrations
{
    public partial class RemoveRedundantColumns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "UsersPlaces");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "UsersPlaces");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "UsersGameTasks");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "UsersGameTasks");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "UsersPlaces",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "UsersPlaces",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "UsersGameTasks",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "UsersGameTasks",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}

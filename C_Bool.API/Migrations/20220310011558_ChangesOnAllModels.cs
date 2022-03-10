using Microsoft.EntityFrameworkCore.Migrations;

namespace C_Bool.API.Migrations
{
    public partial class ChangesOnAllModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GameTaskName",
                table: "UserGameTask");

            migrationBuilder.DropColumn(
                name: "PlaceName",
                table: "UserGameTask");

            migrationBuilder.DropColumn(
                name: "UserName",
                table: "UserGameTask");

            migrationBuilder.DropColumn(
                name: "GameTaskId",
                table: "Places");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Places",
                newName: "PlaceName");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "GameTasks",
                newName: "GameTaskName");

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "GameTaskType",
                table: "UserGameTask",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Places",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserName",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Places");

            migrationBuilder.RenameColumn(
                name: "PlaceName",
                table: "Places",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "GameTaskName",
                table: "GameTasks",
                newName: "Name");

            migrationBuilder.AlterColumn<string>(
                name: "GameTaskType",
                table: "UserGameTask",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "GameTaskName",
                table: "UserGameTask",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PlaceName",
                table: "UserGameTask",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "UserGameTask",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "GameTaskId",
                table: "Places",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}

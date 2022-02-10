using Microsoft.EntityFrameworkCore.Migrations;

namespace C_bool.BLL.Migrations
{
    public partial class AddedNewPropToGameTaskAndUserGameTask : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BonusPoints",
                table: "UsersGameTasks",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsDoneLimited",
                table: "GameTasks",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "LeftDoneAttempts",
                table: "GameTasks",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BonusPoints",
                table: "UsersGameTasks");

            migrationBuilder.DropColumn(
                name: "IsDoneLimited",
                table: "GameTasks");

            migrationBuilder.DropColumn(
                name: "LeftDoneAttempts",
                table: "GameTasks");
        }
    }
}

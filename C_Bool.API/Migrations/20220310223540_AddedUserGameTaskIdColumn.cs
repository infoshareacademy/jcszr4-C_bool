using Microsoft.EntityFrameworkCore.Migrations;

namespace C_Bool.API.Migrations
{
    public partial class AddedUserGameTaskIdColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserGameTaskId",
                table: "UserGameTask",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserGameTaskId",
                table: "UserGameTask");
        }
    }
}

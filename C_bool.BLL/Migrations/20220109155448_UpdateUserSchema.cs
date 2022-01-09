using Microsoft.EntityFrameworkCore.Migrations;

namespace C_bool.BLL.Migrations
{
    public partial class UpdateUserSchema : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Users",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Places",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "GameTasks",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserId1",
                table: "GameTasks",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Places_UserId",
                table: "Places",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_GameTasks_UserId",
                table: "GameTasks",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_GameTasks_UserId1",
                table: "GameTasks",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_GameTasks_Users_UserId",
                table: "GameTasks",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_GameTasks_Users_UserId1",
                table: "GameTasks",
                column: "UserId1",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Places_Users_UserId",
                table: "Places",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GameTasks_Users_UserId",
                table: "GameTasks");

            migrationBuilder.DropForeignKey(
                name: "FK_GameTasks_Users_UserId1",
                table: "GameTasks");

            migrationBuilder.DropForeignKey(
                name: "FK_Places_Users_UserId",
                table: "Places");

            migrationBuilder.DropIndex(
                name: "IX_Places_UserId",
                table: "Places");

            migrationBuilder.DropIndex(
                name: "IX_GameTasks_UserId",
                table: "GameTasks");

            migrationBuilder.DropIndex(
                name: "IX_GameTasks_UserId1",
                table: "GameTasks");

            migrationBuilder.DropColumn(
                name: "Password",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Places");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "GameTasks");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "GameTasks");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(30)",
                oldMaxLength: 30);
        }
    }
}

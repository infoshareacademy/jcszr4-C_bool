using Microsoft.EntityFrameworkCore.Migrations;

namespace C_bool.BLL.Migrations
{
    public partial class AddedUserGameTaskIdToPK : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Messages_UsersGameTasks_UserGameTaskUserId_UserGameTaskGameTaskId",
                table: "Messages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UsersGameTasks",
                table: "UsersGameTasks");

            migrationBuilder.DropIndex(
                name: "IX_Messages_UserGameTaskUserId_UserGameTaskGameTaskId",
                table: "Messages");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "UsersGameTasks",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "UserGameTaskId",
                table: "Messages",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_UsersGameTasks",
                table: "UsersGameTasks",
                columns: new[] { "Id", "UserId", "GameTaskId" });

            migrationBuilder.CreateIndex(
                name: "IX_UsersGameTasks_UserId",
                table: "UsersGameTasks",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_UserGameTaskId_UserGameTaskUserId_UserGameTaskGameTaskId",
                table: "Messages",
                columns: new[] { "UserGameTaskId", "UserGameTaskUserId", "UserGameTaskGameTaskId" });

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_UsersGameTasks_UserGameTaskId_UserGameTaskUserId_UserGameTaskGameTaskId",
                table: "Messages",
                columns: new[] { "UserGameTaskId", "UserGameTaskUserId", "UserGameTaskGameTaskId" },
                principalTable: "UsersGameTasks",
                principalColumns: new[] { "Id", "UserId", "GameTaskId" },
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Messages_UsersGameTasks_UserGameTaskId_UserGameTaskUserId_UserGameTaskGameTaskId",
                table: "Messages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UsersGameTasks",
                table: "UsersGameTasks");

            migrationBuilder.DropIndex(
                name: "IX_UsersGameTasks_UserId",
                table: "UsersGameTasks");

            migrationBuilder.DropIndex(
                name: "IX_Messages_UserGameTaskId_UserGameTaskUserId_UserGameTaskGameTaskId",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "UsersGameTasks");

            migrationBuilder.DropColumn(
                name: "UserGameTaskId",
                table: "Messages");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UsersGameTasks",
                table: "UsersGameTasks",
                columns: new[] { "UserId", "GameTaskId" });

            migrationBuilder.CreateIndex(
                name: "IX_Messages_UserGameTaskUserId_UserGameTaskGameTaskId",
                table: "Messages",
                columns: new[] { "UserGameTaskUserId", "UserGameTaskGameTaskId" });

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_UsersGameTasks_UserGameTaskUserId_UserGameTaskGameTaskId",
                table: "Messages",
                columns: new[] { "UserGameTaskUserId", "UserGameTaskGameTaskId" },
                principalTable: "UsersGameTasks",
                principalColumns: new[] { "UserId", "GameTaskId" },
                onDelete: ReferentialAction.Restrict);
        }
    }
}

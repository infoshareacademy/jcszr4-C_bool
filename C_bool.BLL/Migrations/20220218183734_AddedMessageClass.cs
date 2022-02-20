using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace C_bool.BLL.Migrations
{
    public partial class AddedMessageClass : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BonusPoints",
                table: "UsersGameTasks");

            migrationBuilder.CreateTable(
                name: "Messages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedById = table.Column<int>(type: "int", nullable: false),
                    RootId = table.Column<int>(type: "int", nullable: false),
                    ParentId = table.Column<int>(type: "int", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Body = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GameTaskId = table.Column<int>(type: "int", nullable: true),
                    PlaceId = table.Column<int>(type: "int", nullable: true),
                    UserGameTaskGameTaskId = table.Column<int>(type: "int", nullable: true),
                    UserGameTaskUserId = table.Column<int>(type: "int", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Messages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Messages_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Messages_GameTasks_GameTaskId",
                        column: x => x.GameTaskId,
                        principalTable: "GameTasks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Messages_Places_PlaceId",
                        column: x => x.PlaceId,
                        principalTable: "Places",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Messages_UsersGameTasks_UserGameTaskUserId_UserGameTaskGameTaskId",
                        columns: x => new { x.UserGameTaskUserId, x.UserGameTaskGameTaskId },
                        principalTable: "UsersGameTasks",
                        principalColumns: new[] { "UserId", "GameTaskId" },
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Messages_GameTaskId",
                table: "Messages",
                column: "GameTaskId");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_PlaceId",
                table: "Messages",
                column: "PlaceId");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_UserGameTaskUserId_UserGameTaskGameTaskId",
                table: "Messages",
                columns: new[] { "UserGameTaskUserId", "UserGameTaskGameTaskId" });

            migrationBuilder.CreateIndex(
                name: "IX_Messages_UserId",
                table: "Messages",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Messages");

            migrationBuilder.AddColumn<int>(
                name: "BonusPoints",
                table: "UsersGameTasks",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}

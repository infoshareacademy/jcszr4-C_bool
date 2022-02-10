using Microsoft.EntityFrameworkCore.Migrations;

namespace C_bool.BLL.Migrations
{
    public partial class AddIsActivePropertyToPlace : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                name: "IsActive",
                table: "Places");
        }
    }
}

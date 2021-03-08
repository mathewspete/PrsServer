using Microsoft.EntityFrameworkCore.Migrations;

namespace PrsServer.Migrations
{
    public partial class fixeduseradmin : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "IsAdmin",
                table: "Users",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "IsAdmin",
                table: "Users",
                type: "int",
                nullable: false,
                oldClrType: typeof(bool));
        }
    }
}

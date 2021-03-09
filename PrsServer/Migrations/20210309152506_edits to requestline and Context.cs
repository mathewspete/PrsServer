using Microsoft.EntityFrameworkCore.Migrations;

namespace PrsServer.Migrations
{
    public partial class editstorequestlineandContext : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Quantity",
                table: "RequestLine",
                nullable: false,
                defaultValue: 1,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<decimal>(
                name: "Total",
                table: "Request",
                type: "decimal(11,2)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(11,2)");

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "Request",
                maxLength: 10,
                nullable: false,
                defaultValue: "NEW",
                oldClrType: typeof(string),
                oldType: "nvarchar(10)",
                oldMaxLength: 10);

            migrationBuilder.AlterColumn<string>(
                name: "DeliveryMode",
                table: "Request",
                maxLength: 20,
                nullable: false,
                defaultValue: "Pickup",
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            migrationBuilder.CreateIndex(
                name: "IX_RequestLine_ProductId",
                table: "RequestLine",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_RequestLine_RequestId",
                table: "RequestLine",
                column: "RequestId");

            migrationBuilder.AddForeignKey(
                name: "FK_RequestLine_Product_ProductId",
                table: "RequestLine",
                column: "ProductId",
                principalTable: "Product",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RequestLine_Request_RequestId",
                table: "RequestLine",
                column: "RequestId",
                principalTable: "Request",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RequestLine_Product_ProductId",
                table: "RequestLine");

            migrationBuilder.DropForeignKey(
                name: "FK_RequestLine_Request_RequestId",
                table: "RequestLine");

            migrationBuilder.DropIndex(
                name: "IX_RequestLine_ProductId",
                table: "RequestLine");

            migrationBuilder.DropIndex(
                name: "IX_RequestLine_RequestId",
                table: "RequestLine");

            migrationBuilder.AlterColumn<int>(
                name: "Quantity",
                table: "RequestLine",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldDefaultValue: 1);

            migrationBuilder.AlterColumn<decimal>(
                name: "Total",
                table: "Request",
                type: "decimal(11,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(11,2)",
                oldDefaultValue: 0m);

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "Request",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 10,
                oldDefaultValue: "NEW");

            migrationBuilder.AlterColumn<string>(
                name: "DeliveryMode",
                table: "Request",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 20,
                oldDefaultValue: "Pickup");
        }
    }
}

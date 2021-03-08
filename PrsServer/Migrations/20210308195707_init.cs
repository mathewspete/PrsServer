using Microsoft.EntityFrameworkCore.Migrations;

namespace PrsServer.Migrations {
	public partial class init : Migration {
		protected override void Up(MigrationBuilder migrationBuilder) {
			migrationBuilder.CreateTable(
					name: "Request",
					columns: table => new {
						Id = table.Column<int>(nullable: false)
									.Annotation("SqlServer:Identity", "1, 1"),
						Description = table.Column<string>(maxLength: 80, nullable: false),
						Justification = table.Column<string>(maxLength: 80, nullable: false),
						RejectionReason = table.Column<string>(maxLength: 80, nullable: true),
						DeliveryMode = table.Column<string>(maxLength: 20, nullable: false, defaultValueSql: "'Pickup'"),
						Status = table.Column<string>(maxLength: 10, nullable: false, defaultValueSql: "'NEW'"),
						Total = table.Column<decimal>(type: "decimal(11,2)", nullable: false, defaultValueSql: "0"),
						UserId = table.Column<int>(nullable: false)
					},
					constraints: table => {
						table.PrimaryKey("PK_Request", x => x.Id);
						table.ForeignKey(
											name: "FK_Request_Users_UserId",
											column: x => x.UserId,
											principalTable: "Users",
											principalColumn: "Id",
											onDelete: ReferentialAction.Cascade);
					});

			migrationBuilder.CreateTable(
					name: "RequestLine",
					columns: table => new {
						Id = table.Column<int>(nullable: false)
									.Annotation("SqlServer:Identity", "1, 1"),
						RequestId = table.Column<int>(nullable: false),
						ProductId = table.Column<int>(nullable: false),
						Quantity = table.Column<int>(nullable: false, defaultValueSql: "1")
					},
					constraints: table => {
						table.PrimaryKey("PK_RequestLine", x => x.Id);
					});

			migrationBuilder.CreateTable(
					name: "Vendor",
					columns: table => new {
						Id = table.Column<int>(nullable: false)
									.Annotation("SqlServer:Identity", "1, 1"),
						Code = table.Column<string>(maxLength: 30, nullable: false),
						Name = table.Column<string>(maxLength: 30, nullable: false),
						Address = table.Column<string>(maxLength: 30, nullable: false),
						City = table.Column<string>(maxLength: 30, nullable: false),
						State = table.Column<string>(maxLength: 2, nullable: false),
						Zip = table.Column<string>(maxLength: 5, nullable: false),
						Phone = table.Column<string>(maxLength: 12, nullable: true),
						Email = table.Column<string>(maxLength: 255, nullable: true)
					},
					constraints: table => {
						table.PrimaryKey("PK_Vendor", x => x.Id);
					});

			migrationBuilder.CreateTable(
					name: "Product",
					columns: table => new {
						Id = table.Column<int>(nullable: false)
									.Annotation("SqlServer:Identity", "1, 1"),
						PartNbr = table.Column<string>(maxLength: 30, nullable: false),
						Name = table.Column<string>(maxLength: 30, nullable: false),
						Price = table.Column<decimal>(type: "decimal(9,2)", nullable: false),
						Unit = table.Column<string>(maxLength: 30, nullable: false),
						PhotoPath = table.Column<string>(maxLength: 255, nullable: true),
						VendorId = table.Column<int>(nullable: false)
					},
					constraints: table => {
						table.PrimaryKey("PK_Product", x => x.Id);
						table.ForeignKey(
											name: "FK_Product_Vendor_VendorId",
											column: x => x.VendorId,
											principalTable: "Vendor",
											principalColumn: "Id",
											onDelete: ReferentialAction.Cascade);
					});

			migrationBuilder.CreateIndex(
					name: "IX_Product_PartNbr",
					table: "Product",
					column: "PartNbr",
					unique: true);

			migrationBuilder.CreateIndex(
					name: "IX_Product_VendorId",
					table: "Product",
					column: "VendorId");

			migrationBuilder.CreateIndex(
					name: "IX_Request_UserId",
					table: "Request",
					column: "UserId");

			migrationBuilder.CreateIndex(
					name: "IX_Vendor_Code",
					table: "Vendor",
					column: "Code",
					unique: true);
		}

		protected override void Down(MigrationBuilder migrationBuilder) {
			migrationBuilder.DropTable(
					name: "Product");

			migrationBuilder.DropTable(
					name: "Request");

			migrationBuilder.DropTable(
					name: "RequestLine");

			migrationBuilder.DropTable(
					name: "Vendor");
		}
	}
}

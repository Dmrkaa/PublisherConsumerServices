using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ServiceConsumer.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Organizations",
                columns: table => new
                {
                    OrganizationID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Organizations", x => x.OrganizationID);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrganizationID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    OrganizationInfoKey = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    MiddleName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserID);
                    table.ForeignKey(
                        name: "FK_Users_Organizations_OrganizationInfoKey",
                        column: x => x.OrganizationInfoKey,
                        principalTable: "Organizations",
                        principalColumn: "OrganizationID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Organizations",
                columns: new[] { "OrganizationID", "Name" },
                values: new object[,]
                {
                    { new Guid("ee6afa50-35fc-4e89-82c1-1ad0f953aae7"), "Marvel" },
                    { new Guid("a2033d2d-8cf8-42e5-be9b-11049700fdc1"), "Universal" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserID", "Email", "LastName", "MiddleName", "Name", "OrganizationID", "OrganizationInfoKey" },
                values: new object[,]
                {
                    { new Guid("f3af8321-c917-4d3b-911e-547e28102c90"), "abc@gmail.kz", "Hohland", "Ivanovich", "Tom", null, null },
                    { new Guid("e9aefbfd-45e9-4fb1-bd0d-ebfe36ec6a4f"), "WolfAlice@outllok.com", "Wolf", null, "Alice", null, null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_OrganizationInfoKey",
                table: "Users",
                column: "OrganizationInfoKey");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Organizations");
        }
    }
}

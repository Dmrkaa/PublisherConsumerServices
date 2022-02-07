using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ServiceConsumer.Migrations
{
    public partial class FixFK : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Organizations_OrganizationInfoKey",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_OrganizationInfoKey",
                table: "Users");

            migrationBuilder.DeleteData(
                table: "Organizations",
                keyColumn: "OrganizationID",
                keyValue: new Guid("a2033d2d-8cf8-42e5-be9b-11049700fdc1"));

            migrationBuilder.DeleteData(
                table: "Organizations",
                keyColumn: "OrganizationID",
                keyValue: new Guid("ee6afa50-35fc-4e89-82c1-1ad0f953aae7"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: new Guid("e9aefbfd-45e9-4fb1-bd0d-ebfe36ec6a4f"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: new Guid("f3af8321-c917-4d3b-911e-547e28102c90"));

            migrationBuilder.DropColumn(
                name: "OrganizationInfoKey",
                table: "Users");

            migrationBuilder.InsertData(
                table: "Organizations",
                columns: new[] { "OrganizationID", "Name" },
                values: new object[,]
                {
                    { new Guid("b35a3c95-b96e-4bd0-9a54-86f282ed9543"), "Marvel" },
                    { new Guid("1fecb8bf-bf38-41e7-8146-00ad585c2e18"), "Universal" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserID", "Email", "LastName", "MiddleName", "Name", "OrganizationID" },
                values: new object[,]
                {
                    { new Guid("d30fbdc8-e4e7-4f0a-a95e-8ff01d403872"), "abc@gmail.kz", "Hohland", "Ivanovich", "Tom", null },
                    { new Guid("dd2675c9-3119-4b7f-9aba-34a128b5d4b7"), "WolfAlice@outllok.com", "Wolf", null, "Alice", null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_OrganizationID",
                table: "Users",
                column: "OrganizationID");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Organizations_OrganizationID",
                table: "Users",
                column: "OrganizationID",
                principalTable: "Organizations",
                principalColumn: "OrganizationID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Organizations_OrganizationID",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_OrganizationID",
                table: "Users");

            migrationBuilder.DeleteData(
                table: "Organizations",
                keyColumn: "OrganizationID",
                keyValue: new Guid("1fecb8bf-bf38-41e7-8146-00ad585c2e18"));

            migrationBuilder.DeleteData(
                table: "Organizations",
                keyColumn: "OrganizationID",
                keyValue: new Guid("b35a3c95-b96e-4bd0-9a54-86f282ed9543"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: new Guid("d30fbdc8-e4e7-4f0a-a95e-8ff01d403872"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: new Guid("dd2675c9-3119-4b7f-9aba-34a128b5d4b7"));

            migrationBuilder.AddColumn<Guid>(
                name: "OrganizationInfoKey",
                table: "Users",
                type: "uniqueidentifier",
                nullable: true);

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

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Organizations_OrganizationInfoKey",
                table: "Users",
                column: "OrganizationInfoKey",
                principalTable: "Organizations",
                principalColumn: "OrganizationID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

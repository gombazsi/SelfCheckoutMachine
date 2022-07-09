using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SelfCheckoutMachine.Migrations
{
    public partial class Seed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Currencies",
                columns: new[] { "Id", "Code", "ValueInHuf" },
                values: new object[] { new Guid("960de4c0-bbbe-4e74-b4d5-4c1754f4f9ba"), "HUF", 1m });

            migrationBuilder.InsertData(
                table: "Currencies",
                columns: new[] { "Id", "Code", "ValueInHuf" },
                values: new object[] { new Guid("e8547fe0-e517-45b3-91c1-0e12772211cc"), "EUR", 407.21m });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: new Guid("960de4c0-bbbe-4e74-b4d5-4c1754f4f9ba"));

            migrationBuilder.DeleteData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: new Guid("e8547fe0-e517-45b3-91c1-0e12772211cc"));
        }
    }
}

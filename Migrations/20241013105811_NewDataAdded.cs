using Microsoft.EntityFrameworkCore.Migrations;

namespace learndotnet.Migrations
{
    public partial class NewDataAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "GameTable",
                columns: new[] { "Id", "Device", "Name" },
                values: new object[] { 1, "mobile", "PUBG" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "GameTable",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}

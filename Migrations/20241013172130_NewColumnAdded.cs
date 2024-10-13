using Microsoft.EntityFrameworkCore.Migrations;

namespace learndotnet.Migrations
{
    public partial class NewColumnAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "HashKey",
                table: "GameTable",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "GameTable",
                keyColumn: "Id",
                keyValue: 1,
                column: "HashKey",
                value: "H#adwe@nd");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HashKey",
                table: "GameTable");
        }
    }
}

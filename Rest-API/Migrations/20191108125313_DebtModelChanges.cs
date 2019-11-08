using Microsoft.EntityFrameworkCore.Migrations;

namespace Rest_API.Migrations
{
    public partial class DebtModelChanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsImportant",
                table: "Debts");

            migrationBuilder.AddColumn<int>(
                name: "CreatorId",
                table: "LocalUsers",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "LocalUsers",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsPayed",
                table: "Debts",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatorId",
                table: "LocalUsers");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "LocalUsers");

            migrationBuilder.DropColumn(
                name: "IsPayed",
                table: "Debts");

            migrationBuilder.AddColumn<bool>(
                name: "IsImportant",
                table: "Debts",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}

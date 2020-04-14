using Microsoft.EntityFrameworkCore.Migrations;

namespace Rest_API.Migrations
{
    public partial class DebtModelChanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LocalBorrowerId",
                table: "Debts");

            migrationBuilder.DropColumn(
                name: "LocalLenderId",
                table: "Debts");

            migrationBuilder.AlterColumn<int>(
                name: "LenderId",
                table: "Debts",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "BorrowerId",
                table: "Debts",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsBorrowerLocal",
                table: "Debts",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsLenderLocal",
                table: "Debts",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "Contacts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    Picture = table.Column<string>(nullable: true),
                    CreatorId = table.Column<int>(nullable: false),
                    UserId = table.Column<int>(nullable: false),
                    IsUserLocal = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contacts", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Contacts");

            migrationBuilder.DropColumn(
                name: "IsBorrowerLocal",
                table: "Debts");

            migrationBuilder.DropColumn(
                name: "IsLenderLocal",
                table: "Debts");

            migrationBuilder.AlterColumn<int>(
                name: "LenderId",
                table: "Debts",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "BorrowerId",
                table: "Debts",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "LocalBorrowerId",
                table: "Debts",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LocalLenderId",
                table: "Debts",
                type: "int",
                nullable: true);
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Rest_API.Migrations
{
    public partial class deleteContactPictureAndDebtPaymentDate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DebtPaymentDate",
                table: "Debts");

            migrationBuilder.DropColumn(
                name: "UserPicture",
                table: "Contacts");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DebtPaymentDate",
                table: "Debts",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserPicture",
                table: "Contacts",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}

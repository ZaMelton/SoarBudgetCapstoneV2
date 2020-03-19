using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Repository.Migrations
{
    public partial class AddedBudgetStartDate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Amount",
                table: "DebtItems");

            migrationBuilder.AddColumn<double>(
                name: "AmountToPayPerMonth",
                table: "DebtItems",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "TotalDebtAmount",
                table: "DebtItems",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<DateTime>(
                name: "BudgetStartDate",
                table: "Budgets",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AmountToPayPerMonth",
                table: "DebtItems");

            migrationBuilder.DropColumn(
                name: "TotalDebtAmount",
                table: "DebtItems");

            migrationBuilder.DropColumn(
                name: "BudgetStartDate",
                table: "Budgets");

            migrationBuilder.AddColumn<double>(
                name: "Amount",
                table: "DebtItems",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}

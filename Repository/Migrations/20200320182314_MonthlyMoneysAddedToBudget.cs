using Microsoft.EntityFrameworkCore.Migrations;

namespace Repository.Migrations
{
    public partial class MonthlyMoneysAddedToBudget : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MonthlyBillMoney",
                table: "Wallets");

            migrationBuilder.DropColumn(
                name: "MonthlyBudgetItemMoney",
                table: "Wallets");

            migrationBuilder.DropColumn(
                name: "MonthlyDebtItemMoney",
                table: "Wallets");

            migrationBuilder.DropColumn(
                name: "MonthlyGoalItemMoney",
                table: "Wallets");

            migrationBuilder.DropColumn(
                name: "MonthlyMoneySaved",
                table: "Wallets");

            migrationBuilder.DropColumn(
                name: "MonthlyRandomExpenseMoney",
                table: "Wallets");

            migrationBuilder.DropColumn(
                name: "MonthlyTotalMoney",
                table: "Wallets");

            migrationBuilder.AddColumn<double>(
                name: "MonthlyBillMoney",
                table: "Budgets",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "MonthlyBudgetItemMoney",
                table: "Budgets",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "MonthlyDebtItemMoney",
                table: "Budgets",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "MonthlyGoalItemMoney",
                table: "Budgets",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "MonthlyMoneySaved",
                table: "Budgets",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "MonthlyRandomExpenseMoney",
                table: "Budgets",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "MonthlyTotalMoney",
                table: "Budgets",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MonthlyBillMoney",
                table: "Budgets");

            migrationBuilder.DropColumn(
                name: "MonthlyBudgetItemMoney",
                table: "Budgets");

            migrationBuilder.DropColumn(
                name: "MonthlyDebtItemMoney",
                table: "Budgets");

            migrationBuilder.DropColumn(
                name: "MonthlyGoalItemMoney",
                table: "Budgets");

            migrationBuilder.DropColumn(
                name: "MonthlyMoneySaved",
                table: "Budgets");

            migrationBuilder.DropColumn(
                name: "MonthlyRandomExpenseMoney",
                table: "Budgets");

            migrationBuilder.DropColumn(
                name: "MonthlyTotalMoney",
                table: "Budgets");

            migrationBuilder.AddColumn<double>(
                name: "MonthlyBillMoney",
                table: "Wallets",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "MonthlyBudgetItemMoney",
                table: "Wallets",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "MonthlyDebtItemMoney",
                table: "Wallets",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "MonthlyGoalItemMoney",
                table: "Wallets",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "MonthlyMoneySaved",
                table: "Wallets",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "MonthlyRandomExpenseMoney",
                table: "Wallets",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "MonthlyTotalMoney",
                table: "Wallets",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}

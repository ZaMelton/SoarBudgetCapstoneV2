using Microsoft.EntityFrameworkCore.Migrations;

namespace Repository.Migrations
{
    public partial class AddedMonthlyMoneysToWallet : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BillMoney",
                table: "Wallets");

            migrationBuilder.DropColumn(
                name: "BudgetItemMoney",
                table: "Wallets");

            migrationBuilder.DropColumn(
                name: "DebtItemMoney",
                table: "Wallets");

            migrationBuilder.DropColumn(
                name: "GoalItemMoney",
                table: "Wallets");

            migrationBuilder.DropColumn(
                name: "MoneySaved",
                table: "Wallets");

            migrationBuilder.DropColumn(
                name: "RandomExpenseMoney",
                table: "Wallets");

            migrationBuilder.AddColumn<double>(
                name: "MonthlyBillMoney",
                table: "Wallets",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "MonthlyBudgetItemMoney",
                table: "Wallets",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "MonthlyDebtItemMoney",
                table: "Wallets",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "MonthlyGoalItemMoney",
                table: "Wallets",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "MonthlyMoneySaved",
                table: "Wallets",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "MonthlyRandomExpenseMoney",
                table: "Wallets",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "MonthlyTotalMoney",
                table: "Wallets",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "TotalBillMoney",
                table: "Wallets",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "TotalBudgetItemMoney",
                table: "Wallets",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "TotalDebtItemMoney",
                table: "Wallets",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "TotalGoalItemMoney",
                table: "Wallets",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "TotalMoneySaved",
                table: "Wallets",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "TotalRandomExpenseMoney",
                table: "Wallets",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "TotalBillMoney",
                table: "Wallets");

            migrationBuilder.DropColumn(
                name: "TotalBudgetItemMoney",
                table: "Wallets");

            migrationBuilder.DropColumn(
                name: "TotalDebtItemMoney",
                table: "Wallets");

            migrationBuilder.DropColumn(
                name: "TotalGoalItemMoney",
                table: "Wallets");

            migrationBuilder.DropColumn(
                name: "TotalMoneySaved",
                table: "Wallets");

            migrationBuilder.DropColumn(
                name: "TotalRandomExpenseMoney",
                table: "Wallets");

            migrationBuilder.AddColumn<double>(
                name: "BillMoney",
                table: "Wallets",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "BudgetItemMoney",
                table: "Wallets",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "DebtItemMoney",
                table: "Wallets",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "GoalItemMoney",
                table: "Wallets",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "MoneySaved",
                table: "Wallets",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "RandomExpenseMoney",
                table: "Wallets",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

namespace Repository.Migrations
{
    public partial class FixedNameForBudgetItemExpense : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BudgetItemName",
                table: "BudgetItemExpenses");

            migrationBuilder.AddColumn<string>(
                name: "BudgetItemExpenseName",
                table: "BudgetItemExpenses",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BudgetItemExpenseName",
                table: "BudgetItemExpenses");

            migrationBuilder.AddColumn<string>(
                name: "BudgetItemName",
                table: "BudgetItemExpenses",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}

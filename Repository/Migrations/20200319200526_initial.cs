using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Repository.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Bills",
                columns: table => new
                {
                    BillId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BillName = table.Column<string>(nullable: false),
                    BillType = table.Column<string>(nullable: false),
                    Amount = table.Column<double>(nullable: false),
                    DueDate = table.Column<DateTime>(nullable: false),
                    BudgeteerId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bills", x => x.BillId);
                });

            migrationBuilder.CreateTable(
                name: "BudgetItems",
                columns: table => new
                {
                    BudgetItemId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BudgetItemName = table.Column<string>(nullable: false),
                    Category = table.Column<string>(nullable: false),
                    Amount = table.Column<double>(nullable: false),
                    BudgeteerId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BudgetItems", x => x.BudgetItemId);
                });

            migrationBuilder.CreateTable(
                name: "Budgets",
                columns: table => new
                {
                    BudgetId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MonthlyIncome = table.Column<double>(nullable: false),
                    MonthlyLimit = table.Column<double>(nullable: false),
                    RandomExpenseLimit = table.Column<double>(nullable: false),
                    MonthId = table.Column<int>(nullable: false),
                    Year = table.Column<int>(nullable: false),
                    BudgeteerId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Budgets", x => x.BudgetId);
                });

            migrationBuilder.CreateTable(
                name: "DebtItems",
                columns: table => new
                {
                    DebtItemId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DebtItemName = table.Column<string>(nullable: false),
                    Category = table.Column<string>(nullable: false),
                    Amount = table.Column<double>(nullable: false),
                    BudgeteerId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DebtItems", x => x.DebtItemId);
                });

            migrationBuilder.CreateTable(
                name: "GoalItems",
                columns: table => new
                {
                    GoalItemId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GoalItemName = table.Column<string>(nullable: false),
                    Category = table.Column<string>(nullable: false),
                    Amount = table.Column<double>(nullable: false),
                    BudgeteerId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GoalItems", x => x.GoalItemId);
                });

            migrationBuilder.CreateTable(
                name: "RandomExpenses",
                columns: table => new
                {
                    RandomExpenseId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RandomExpenseName = table.Column<string>(nullable: false),
                    Category = table.Column<string>(nullable: false),
                    Amount = table.Column<double>(nullable: false),
                    BudgetId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RandomExpenses", x => x.RandomExpenseId);
                });

            migrationBuilder.CreateTable(
                name: "Wallets",
                columns: table => new
                {
                    WalletId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TotalMoney = table.Column<double>(nullable: false),
                    BudgetItemMoney = table.Column<double>(nullable: false),
                    DebtItemMoney = table.Column<double>(nullable: false),
                    BillMoney = table.Column<double>(nullable: false),
                    GoalItemMoney = table.Column<double>(nullable: false),
                    RandomExpenseMoney = table.Column<double>(nullable: false),
                    MoneySaved = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Wallets", x => x.WalletId);
                });

            migrationBuilder.CreateTable(
                name: "Budgeteers",
                columns: table => new
                {
                    BudgeteerId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(nullable: true),
                    FirstName = table.Column<string>(nullable: false),
                    LastName = table.Column<string>(nullable: false),
                    Email = table.Column<string>(nullable: false),
                    PhoneNumber = table.Column<string>(nullable: false),
                    WalletId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Budgeteers", x => x.BudgeteerId);
                    table.ForeignKey(
                        name: "FK_Budgeteers_Wallets_WalletId",
                        column: x => x.WalletId,
                        principalTable: "Wallets",
                        principalColumn: "WalletId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Budgeteers_WalletId",
                table: "Budgeteers",
                column: "WalletId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Bills");

            migrationBuilder.DropTable(
                name: "Budgeteers");

            migrationBuilder.DropTable(
                name: "BudgetItems");

            migrationBuilder.DropTable(
                name: "Budgets");

            migrationBuilder.DropTable(
                name: "DebtItems");

            migrationBuilder.DropTable(
                name: "GoalItems");

            migrationBuilder.DropTable(
                name: "RandomExpenses");

            migrationBuilder.DropTable(
                name: "Wallets");
        }
    }
}

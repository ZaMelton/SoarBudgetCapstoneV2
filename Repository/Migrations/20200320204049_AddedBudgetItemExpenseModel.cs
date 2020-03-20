using Microsoft.EntityFrameworkCore.Migrations;

namespace Repository.Migrations
{
    public partial class AddedBudgetItemExpenseModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BudgetItemExpenses",
                columns: table => new
                {
                    BudgetItemExpenseId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BudgetItemName = table.Column<string>(nullable: false),
                    Category = table.Column<string>(nullable: false),
                    Amount = table.Column<double>(nullable: false),
                    BudgetId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BudgetItemExpenses", x => x.BudgetItemExpenseId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BudgetItemExpenses");
        }
    }
}

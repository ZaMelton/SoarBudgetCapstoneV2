using Microsoft.EntityFrameworkCore.Migrations;

namespace Repository.Migrations
{
    public partial class AddedCategoryItemsToBudget : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "CoffeeCategoryLimit",
                table: "Budgets",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "CoffeeCategorySpent",
                table: "Budgets",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "DiningOutCategoryLimit",
                table: "Budgets",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "DiningOutCategorySpent",
                table: "Budgets",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "EntertainmentCategoryLimit",
                table: "Budgets",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "EntertainmentCategorySpent",
                table: "Budgets",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "GasCategoryLimit",
                table: "Budgets",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "GasCategorySpent",
                table: "Budgets",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "GroceriesCategoryLimit",
                table: "Budgets",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "GroceriesCategorySpent",
                table: "Budgets",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CoffeeCategoryLimit",
                table: "Budgets");

            migrationBuilder.DropColumn(
                name: "CoffeeCategorySpent",
                table: "Budgets");

            migrationBuilder.DropColumn(
                name: "DiningOutCategoryLimit",
                table: "Budgets");

            migrationBuilder.DropColumn(
                name: "DiningOutCategorySpent",
                table: "Budgets");

            migrationBuilder.DropColumn(
                name: "EntertainmentCategoryLimit",
                table: "Budgets");

            migrationBuilder.DropColumn(
                name: "EntertainmentCategorySpent",
                table: "Budgets");

            migrationBuilder.DropColumn(
                name: "GasCategoryLimit",
                table: "Budgets");

            migrationBuilder.DropColumn(
                name: "GasCategorySpent",
                table: "Budgets");

            migrationBuilder.DropColumn(
                name: "GroceriesCategoryLimit",
                table: "Budgets");

            migrationBuilder.DropColumn(
                name: "GroceriesCategorySpent",
                table: "Budgets");
        }
    }
}

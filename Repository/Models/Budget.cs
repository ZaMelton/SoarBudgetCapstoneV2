using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Repository.Models
{
    public class Budget
    {
        [Key]
        public int BudgetId { get; set; }

        // Monthly limits here
        [Required]
        [Display(Name = "Monthly Income")]
        public double MonthlyIncome { get; set; }

        [Required]
        [Display(Name = "Monthly Limit")]
        public double MonthlyLimit { get; set; }

        [Required]
        [Display(Name = "Random Expense Limit")]
        public double RandomExpenseLimit { get; set; }

        //To track category Limits
        public double CoffeeCategoryLimit { get; set; }
        public double DiningOutCategoryLimit { get; set; }
        public double EntertainmentCategoryLimit { get; set; }
        public double GasCategoryLimit { get; set; }
        public double GroceriesCategoryLimit { get; set; }

        // To track category spending
        public double CoffeeCategorySpent { get; set; }
        public double DiningOutCategorySpent { get; set; }
        public double EntertainmentCategorySpent { get; set; }
        public double GasCategorySpent { get; set; }
        public double GroceriesCategorySpent { get; set; }

        //Area below is for all tracking of money spend in total categories to build the pie chart
        [Display(Name = "Monthly Total Money")]
        public double MonthlyTotalMoney { get; set; }

        [Display(Name = "Monthly Budget Item Money")]
        public double MonthlyBudgetItemMoney { get; set; }

        [Display(Name = "Monthly Debt Item Money")]
        public double MonthlyDebtItemMoney { get; set; }

        [Display(Name = "Monthly Bill Money")]
        public double MonthlyBillMoney { get; set; }

        [Display(Name = "Monthly Goal Item Money")]
        public double MonthlyGoalItemMoney { get; set; }

        [Display(Name = "Monthly Random Expense Money")]
        public double MonthlyRandomExpenseMoney { get; set; }

        [Display(Name = "Monthly Money Saved")]
        public double MonthlyMoneySaved { get; set; }

        //These items are for keeping track of specific budgets
        public DateTime BudgetStartDate { get; set; }
        public int MonthId { get; set; }
        public  int Year { get; set; }

        [ForeignKey("Budgeteer")]
        public int? BudgeteerId { get; set; }
    }
}

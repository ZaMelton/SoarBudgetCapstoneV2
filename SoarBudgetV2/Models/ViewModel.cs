using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoarBudgetV2.Models
{
    public class ViewModel
    {
        public Budgeteer Budgeteer { get; set; }
        public Budget Budget { get; set; }
        public Wallet Wallet { get; set; }
        public RandomExpense RandomExpense { get; set; }
        public BudgetItemExpense BudgetItemExpense { get; set; }
        public List<Bill> Bills { get; set; }
        public List<DebtItem> DebtItems { get; set; }
        public List<BudgetItem> BudgetItems { get; set; }
        public List<GoalItem> GoalItems { get; set; }
        public List<RandomExpense> RandomExpenses { get; set; }
        public List<BudgetItemExpense> BudgetItemExpenses { get; set; }
    }
}

using Repository.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Contracts
{
    public interface IBudgetItemExpenseRepository : IRepositoryBase<BudgetItemExpense>
    {
        public void CreateBudgetItemExpense(BudgetItemExpense budgetItemExpense);
        public BudgetItemExpense GetBudgetItemExpense(int budgetItemExpenseId);
        public List<BudgetItemExpense> GetAllBudgetItemExpensesForBudget(int budgetId);
    }
}

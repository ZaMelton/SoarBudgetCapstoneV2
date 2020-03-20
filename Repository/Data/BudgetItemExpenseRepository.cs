using Repository.Contracts;
using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Repository.Data
{
    public class BudgetItemExpenseRepository : RepositoryBase<BudgetItemExpense>, IBudgetItemExpenseRepository
    {
        public BudgetItemExpenseRepository(RepositoryDbContext repositoryDbContext) : base(repositoryDbContext)
        {

        }

        public void CreateBudgetItemExpense(BudgetItemExpense budgetItemExpense)
        {
            Create(budgetItemExpense);
        }

        public List<BudgetItemExpense> GetAllBudgetItemExpensesForBudget(int budgetId)
        {
            return FindAll().Where(b => b.BudgetId == budgetId).ToList();
        }

        public BudgetItemExpense GetBudgetItemExpense(int budgetItemExpenseId)
        {
            return FindByCondition(b => b.BudgetItemExpenseId == budgetItemExpenseId).FirstOrDefault();
        }
    }
}

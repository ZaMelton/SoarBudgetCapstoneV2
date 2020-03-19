using Repository.Contracts;
using Repository.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Repository.Data
{
    public class RandomExpenseRepository : RepositoryBase<RandomExpense>, IRandomExpenseRepository
    {
        public RandomExpenseRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {

        }

        public void CreateRandomExpense(RandomExpense randomExpense)
        {
            Create(randomExpense);
        }

        public List<RandomExpense> GetAllRandomExpensesForBudget(int budgetId)
        {
            return FindAll().Where(r => r.BudgetId == budgetId).ToList();
        }

        public RandomExpense GetRandomExpense(int randomExpenseId)
        {
            return FindByCondition(r => r.RandomExpenseId == randomExpenseId).FirstOrDefault();
        }
    }
}

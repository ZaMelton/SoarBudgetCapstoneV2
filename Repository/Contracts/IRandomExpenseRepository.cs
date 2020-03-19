using Repository.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Contracts
{
    public interface IRandomExpenseRepository : IRepositoryBase<RandomExpense>
    {
        public void CreateRandomExpense(RandomExpense randomExpense);
        public RandomExpense GetRandomExpense(int randomExpenseId);
        public List<RandomExpense> GetAllRandomExpensesForBudget(int budgetId);
    }
}

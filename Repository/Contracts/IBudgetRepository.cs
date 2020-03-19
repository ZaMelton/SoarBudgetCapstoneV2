using Repository.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Contracts
{
    public interface IBudgetRepository : IRepositoryBase<Budget>
    {
        public void CreateBudget(Budget budget);
        public Budget GetBudget(int budgetId);
        public List<Budget> GetAllBudgetsForBudgeteer(int budgeteerId);
    }
}

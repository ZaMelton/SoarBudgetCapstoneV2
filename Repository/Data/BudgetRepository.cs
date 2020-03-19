using Repository.Contracts;
using Repository.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Repository.Data
{
    public class BudgetRepository : RepositoryBase<Budget>, IBudgetRepository
    {
        public BudgetRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {

        }

        public void CreateBudget(Budget budget)
        {
            Create(budget);
        }

        public List<Budget> GetAllBudgetsForBudgeteer(int budgeteerId)
        {
            return FindAll().Where(b => b.BudgeteerId == budgeteerId).ToList();
        }

        public Budget GetBudget(int budgetId)
        {
            return FindByCondition(b => b.BudgetId == budgetId).FirstOrDefault();
        }
    }
}

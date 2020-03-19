using Repository.Contracts;
using Repository.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Repository.Data
{
    public class BudgetItemRepository : RepositoryBase<BudgetItem>, IBudgetItemRepository
    {
        public BudgetItemRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {

        }

        public void CreateBudgetItem(BudgetItem budgetItem)
        {
            Create(budgetItem);
        }

        public List<BudgetItem> GetAllBudgetItemsForBudgeteer(int budgeteerId)
        {
            return FindAll().Where(b => b.BudgeteerId == budgeteerId).ToList();
        }

        public BudgetItem GetBudgetItem(int budgetItemId)
        {
            return FindByCondition(b => b.BudgetItemId == budgetItemId).FirstOrDefault();
        }
    }
}

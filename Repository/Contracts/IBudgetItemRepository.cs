using Repository.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Contracts
{
    public interface IBudgetItemRepository : IRepositoryBase<BudgetItem>
    {
        public void CreateBudgetItem(BudgetItem budgetItem);
        public BudgetItem GetBudgetItem(int budgetItemId);
        public List<BudgetItem> GetAllBudgetItemsForBudgeteer(int budgeteerId);
    }
}

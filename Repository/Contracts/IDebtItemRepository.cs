using Repository.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Contracts
{
    public interface IDebtItemRepository : IRepositoryBase<DebtItem>
    {
        public void CreateDebtItem(DebtItem debtItem);
        public DebtItem GetDebtItem(int debtItemId);
        public List<DebtItem> GetAllDebtItemsForBudgeteer(int budgeteerId);
    }
}

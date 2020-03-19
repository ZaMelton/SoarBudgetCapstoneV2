using Repository.Contracts;
using Repository.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Repository.Data
{
    public class DebtItemRepository : RepositoryBase<DebtItem>, IDebtItemRepository
    {
        public DebtItemRepository(RepositoryDbContext applicationDbContext) : base(applicationDbContext)
        {

        }

        public void CreateDebtItem(DebtItem debtItem)
        {
            Create(debtItem);
        }

        public List<DebtItem> GetAllDebtItemsForBudgeteer(int budgeteerId)
        {
            return FindAll().Where(d => d.BudgeteerId == budgeteerId).ToList();
        }

        public DebtItem GetDebtItem(int debtItemId)
        {
            return FindByCondition(d => d.DebtItemId == debtItemId).FirstOrDefault();
        }
    }
}

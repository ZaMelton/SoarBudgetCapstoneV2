using Repository.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Contracts
{
    public interface IBudgeteerRepository : IRepositoryBase<Budgeteer>
    {
        public void CreateBudgeteer(Budgeteer budgeteer);
        public Budgeteer GetBudgeteer(int budgeteerId);
        public Budgeteer GetBudgeteerByUserId(string userId);
        public List<Budgeteer> GetAllBudgeteers();
    }
}

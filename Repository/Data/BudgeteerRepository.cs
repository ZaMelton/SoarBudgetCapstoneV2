using Microsoft.EntityFrameworkCore;
using Repository.Contracts;
using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Repository.Data
{
    public class BudgeteerRepository : RepositoryBase<Budgeteer>, IBudgeteerRepository
    {
        public BudgeteerRepository(RepositoryDbContext applicationDbContext) : base(applicationDbContext)
        {

        }

        public void CreateBudgeteer(Budgeteer budgeteer)
        {
            Create(budgeteer);
        }

        public List<Budgeteer> GetAllBudgeteers()
        {
            return FindAll().Include(b => b.Wallet).ToList();
        }

        public Budgeteer GetBudgeteer(int budgeteerId)
        {
            return FindByCondition(b => b.BudgeteerId == budgeteerId).Include(b => b.Wallet).FirstOrDefault();
        }

        public Budgeteer GetBudgeteerByUserId(string userId)
        {
            return FindByCondition(b => b.UserId == userId).Include(b => b.Wallet).FirstOrDefault();
        }
    }
}

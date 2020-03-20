using Repository.Contracts;
using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Repository.Data
{
    public class BillRepository : RepositoryBase<Bill>, IBillRepository
    {
        public BillRepository(RepositoryDbContext repositoryDbContext) : base(repositoryDbContext)
        {

        }

        public void CreateBill(Bill bill)
        {
            Create(bill);
        }

        public List<Bill> GetAllBillsForBudgeteer(int budgeteerId)
        {
            return FindAll().Where(b => b.BudgeteerId == budgeteerId).ToList();
        }

        public Bill GetBill(int billId)
        {
            return FindByCondition(b => b.BillId == billId).FirstOrDefault();
        }
    }
}

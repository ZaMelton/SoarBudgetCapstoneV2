using Repository.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Contracts
{
    public interface IBillRepository : IRepositoryBase<Bill>
    {
        public void CreateBill(Bill bill);
        public Bill GetBill(int billId);
        public List<Bill> GetAllBillsForBudgeteer(int budgeteerId);
    }
}

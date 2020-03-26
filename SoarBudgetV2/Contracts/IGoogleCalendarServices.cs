using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoarBudgetV2.Contracts
{
    public interface IGoogleCalendarServices
    {
        void AddBillEvent(Bill bill);
        void AddDebtItemEvent(DebtItem debtItem);
    }
}

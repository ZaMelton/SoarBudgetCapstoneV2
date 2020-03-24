using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoarBudgetV2.Contracts
{
    public interface ISmsServices
    {
        Task SendSMS(Budgeteer budgeteer, List<string> upcomingBills);
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Repository.Models
{
    public class Wallet
    {
        [Key]
        public int WalletId { get; set; }

        [Display(Name = "Total Money")]
        public double TotalMoney { get; set; }

        [Display(Name = "Budget Item Money")]
        public double BudgetItemMoney { get; set; }

        [Display(Name = "Debt Item Money")]
        public double DebtItemMoney { get; set; }

        [Display(Name = "Bill Money")]
        public double BillMoney { get; set; }

        [Display(Name = "Goal Item Money")]
        public double GoalItemMoney { get; set; }

        [Display(Name = "Random Expense Money")]
        public double RandomExpenseMoney { get; set; }

        [Display(Name = "Money Saved")]
        public double MoneySaved { get; set; }
    }
}

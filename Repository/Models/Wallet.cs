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

        [Display(Name = "Total Budget Item Money")]
        public double TotalBudgetItemMoney { get; set; }

        [Display(Name = "Total Debt Item Money")]
        public double TotalDebtItemMoney { get; set; }

        [Display(Name = "Total Bill Money")]
        public double TotalBillMoney { get; set; }

        [Display(Name = "Total Goal Item Money")]
        public double TotalGoalItemMoney { get; set; }

        [Display(Name = "Total Random Expense Money")]
        public double TotalRandomExpenseMoney { get; set; }

        [Display(Name = "Total Money Saved")]
        public double TotalMoneySaved { get; set; }

        [Display(Name = "Extra Cash")]
        public double ExtraCash { get; set; }
    }
}

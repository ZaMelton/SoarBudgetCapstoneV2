using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Repository.Models
{
    public class Budget
    {
        [Key]
        public int BudgetId { get; set; }

        [Required]
        [Display(Name = "Monthly Income")]
        public double MonthlyIncome { get; set; }

        [Required]
        [Display(Name = "Monthly Limit")]
        public double MonthlyLimit { get; set; }

        [Required]
        [Display(Name = "Random Expense Limit")]
        public double RandomExpenseLimit { get; set; }

        public int MonthId { get; set; }

        public  int Year { get; set; }

        [ForeignKey("Budgeteer")]
        public int? BudgeteerId { get; set; }
    }
}

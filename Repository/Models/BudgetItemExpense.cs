using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Repository.Models
{
    public class BudgetItemExpense
    {
        [Key]
        public int BudgetItemExpenseId { get; set; }

        [Required]
        [Display(Name = "Budget Item Expense Name")]
        public string BudgetItemExpenseName { get; set; }

        [Required]
        public string Category { get; set; }

        [Required]
        public double Amount { get; set; }

        [ForeignKey("Budget")]
        public int? BudgetId { get; set; }
    }
}

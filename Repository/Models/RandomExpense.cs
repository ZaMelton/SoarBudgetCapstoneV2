using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Repository.Models
{
    public class RandomExpense
    {
        [Key]
        public int RandomExpenseId { get; set; }

        [Required]
        [Display(Name = "Random Expense")]
        public string RandomExpenseName { get; set; }

        [Required]
        public string Category { get; set; }

        [Required]
        public double Amount { get; set; }

        [ForeignKey("Budget")]
        public int? BudgetId { get; set; }
    }
}

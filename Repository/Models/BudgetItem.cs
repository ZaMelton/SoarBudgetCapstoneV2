using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Repository.Models
{
    public class BudgetItem
    {
        [Key]
        public int BudgetItemId { get; set; }

        [Required]
        [Display(Name = "Budget Item Name")]
        public string BudgetItemName { get; set; }

        [Required]
        public string Category { get; set; }

        [Required]
        public double Amount { get; set; }

        [ForeignKey("Budgeteer")]
        public int? BudgeteerId { get; set; }
    }
}

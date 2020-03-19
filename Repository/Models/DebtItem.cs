using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Repository.Models
{
    public class DebtItem
    {
        [Key]
        public int DebtItemId { get; set; }

        [Required]
        [Display(Name = "Debt Item Name")]
        public string DebtItemName { get; set; }

        [Required]
        public string Category { get; set; }

        [Required]
        public double Amount { get; set; }

        [ForeignKey("Budgeteer")]
        public int? BudgeteerId { get; set; }
    }
}

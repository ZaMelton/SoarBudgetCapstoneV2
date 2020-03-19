using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Repository.Models
{
    public class GoalItem
    {
        [Key]
        public int GoalItemId { get; set; }

        [Required]
        [Display(Name = "Goal Item Name")]
        public string GoalItemName { get; set; }

        [Required]
        public string Category { get; set; }

        [Required]
        public double Amount { get; set; }

        [ForeignKey("Budgeteer")]
        public int? BudgeteerId { get; set; }
    }
}

﻿using System;
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
        [Display(Name = "Debts")]
        public string DebtItemName { get; set; }

        [Required]
        public string Category { get; set; }

        [Required]
        [Display(Name = "Amount")]
        public double AmountToPayPerMonth { get; set; }

        [Required]
        [Display(Name = "Due Date")]
        public DateTime DueDate { get; set; }

        [Required]
        [Display(Name = "Debt Amount")]
        public double TotalDebtAmount { get; set; }

        public bool IsPaid { get; set; }

        [ForeignKey("Budgeteer")]
        public int? BudgeteerId { get; set; }
    }
}

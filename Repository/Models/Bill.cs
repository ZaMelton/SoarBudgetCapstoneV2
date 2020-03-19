using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Repository.Models
{
    public class Bill
    {
        [Key]
        public int BillId { get; set; }

        [Required]
        [Display(Name = "Bill Name")]
        public string BillName { get; set; }

        [Required]
        [Display(Name = "Bill Type")]
        public string BillType { get; set; }

        [Required]
        public double Amount { get; set; }

        [Required]
        [Display(Name = "Bill Due Date")]
        public DateTime DueDate { get; set; }

        [ForeignKey("Budgeteer")]
        public int? BudgeteerId { get; set; }
    }
}

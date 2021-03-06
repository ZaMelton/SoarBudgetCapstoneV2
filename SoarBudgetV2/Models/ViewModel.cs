﻿using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoarBudgetV2.Models
{
    public class ViewModel
    {
        public Budgeteer Budgeteer { get; set; }
        public Budget Budget { get; set; }
        public Wallet Wallet { get; set; }
        public RandomExpense RandomExpense { get; set; }
        public BudgetItemExpense BudgetItemExpense { get; set; }
        public Bill Bill { get; set; }
        public DebtItem DebtItem { get; set; }
        public GoalItem GoalItem { get; set; }
        public BudgetItem BudgetItem { get; set; }
        public List<Bill> Bills { get; set; }
        public List<DebtItem> DebtItems { get; set; }
        public List<BudgetItem> BudgetItems { get; set; }
        public List<GoalItem> GoalItems { get; set; }
        public List<RandomExpense> RandomExpenses { get; set; }
        public List<BudgetItemExpense> BudgetItemExpenses { get; set; }
        public List<string> UpcomingBills { get; set; }
        public List<string> LateBills { get; set; }
        public List<string> OverspendingAlerts { get; set; }
        public List<string> ApproachingAlerts { get; set; }
    }
}

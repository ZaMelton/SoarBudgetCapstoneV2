﻿using Microsoft.EntityFrameworkCore;
using Repository.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options)
            : base(options)
        {

        }

        public DbSet<Bill> Bills { get; set; }
        public DbSet<Budget> Budgets { get; set; }
        public DbSet<Budgeteer> Budgeteers { get; set; }
        public DbSet<BudgetItem> BudgetItems { get; set; }
        public DbSet<DebtItem> DebtItems { get; set; }
        public DbSet<GoalItem> GoalItems { get; set; }
        public DbSet<RandomExpense> RandomExpenses { get; set; }
        public DbSet<Wallet> Wallets { get; set; }
    }
}

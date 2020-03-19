using Repository.Contracts;
using Repository.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private ApplicationDbContext _context;
        private IBillRepository _bill;
        private IBudgetRepository _budget;
        private IBudgeteerRepository _budgeteer;
        private IBudgetItemRepository _budgetItem;
        private IDebtItemRepository _debtItem;
        private IGoalItemRepository _goalItem;
        private IRandomExpenseRepository _randomExpense;
        private IWalletRepository _wallet;

        public IBillRepository Bills 
        {
            get
            {
                if (_bill == null)
                {
                    _bill = new BillRepository(_context);
                }
                return _bill;
            }
        }
        public IBudgetRepository Budgets 
        {
            get
            {
                if (_budget == null)
                {
                    _budget = new BudgetRepository(_context);
                }
                return _budget;
            } 
        }
        public IBudgeteerRepository Budgeteers 
        {
            get
            {
                if (_budgeteer == null)
                {
                    _budgeteer = new BudgeteerRepository(_context);
                }
                return _budgeteer;
            } 
        }
        public IBudgetItemRepository BudgetItems 
        {
            get
            {
                if (_budgetItem == null)
                {
                    _budgetItem = new BudgetItemRepository(_context);
                }
                return _budgetItem;
            }
        }
        public IDebtItemRepository DebtItems 
        {
            get
            {
                if (_debtItem == null)
                {
                    _debtItem = new DebtItemRepository(_context);
                }
                return _debtItem;
            } 
        }
        public IGoalItemRepository GoalItems 
        {
            get
            {
                if (_goalItem == null)
                {
                    _goalItem = new GoalItemRepository(_context);
                }
                return _goalItem;
            }
        }
        public IRandomExpenseRepository RandomExpenses 
        {
            get
            {
                if (_randomExpense == null)
                {
                    _randomExpense = new RandomExpenseRepository(_context);
                }
                return _randomExpense;
            } 
        }
        public IWalletRepository Wallets 
        {
            get
            {
                if (_wallet == null)
                {
                    _wallet = new WalletRepository(_context);
                }
                return _wallet;
            } 
        }
        public RepositoryWrapper(ApplicationDbContext context)
        {
            _context = context;
        }
        public void Save()
        {
            _context.SaveChanges();
        }
    }
}

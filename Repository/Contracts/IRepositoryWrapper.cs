using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Contracts
{
    public interface IRepositoryWrapper
    {
        IBillRepository Bills { get; }
        IBudgetRepository Budgets { get; }
        IBudgeteerRepository Budgeteers { get; }
        IBudgetItemRepository BudgetItems { get; }
        IDebtItemRepository DebtItems { get; }
        IGoalItemRepository GoalItems { get; }
        IRandomExpenseRepository RandomExpenses { get; }
        IBudgetItemExpenseRepository BudgetItemExpenses { get; }
        IWalletRepository Wallets { get; }
        void Save();
    }
}

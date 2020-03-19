using Repository.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Contracts
{
    public interface IGoalItemRepository : IRepositoryBase<GoalItem>
    {
        public void CreateGoalItem(GoalItem goalItem);
        public GoalItem GetGoalItem(int goalItemId);
        public List<GoalItem> GetAllGoalItemsForBudgeteer(int budgeteerId);
    }
}

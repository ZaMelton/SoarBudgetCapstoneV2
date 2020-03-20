using Repository.Contracts;
using Repository.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Repository.Data
{
    public class GoalItemRepository : RepositoryBase<GoalItem>, IGoalItemRepository
    {
        public GoalItemRepository(RepositoryDbContext repositoryDbContext) : base(repositoryDbContext)
        {

        }

        public void CreateGoalItem(GoalItem goalItem)
        {
            Create(goalItem);
        }

        public List<GoalItem> GetAllGoalItemsForBudgeteer(int budgeteerId)
        {
            return FindAll().Where(g => g.BudgeteerId == budgeteerId).ToList();
        }

        public GoalItem GetGoalItem(int goalItemId)
        {
            return FindByCondition(g => g.GoalItemId == goalItemId).FirstOrDefault();
        }
    }
}

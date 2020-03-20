using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository.Contracts;
using Repository.Models;
using SoarBudgetV2.Models;

namespace SoarBudgetV2.Controllers
{
    public class BudgeteerController : Controller
    {
        private readonly IRepositoryWrapper _repo;

        public BudgeteerController(IRepositoryWrapper repo)
        {
            _repo = repo;
        }

        // GET: Budgeteer
        public ActionResult Index()
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (_repo.Budgeteers.FindByCondition(b => b.UserId == userId).Any())
            {
                var budgeteer = _repo.Budgeteers.GetBudgeteerByUserId(userId);
                var budget = GetCheckAndCreateBudget(budgeteer);
                var wallet = _repo.Wallets.GetWallet(budgeteer.WalletId);
                var bills = _repo.Bills.GetAllBillsForBudgeteer(budgeteer.BudgeteerId);
                var budgetItems = _repo.BudgetItems.GetAllBudgetItemsForBudgeteer(budgeteer.BudgeteerId);
                var debtItems = _repo.DebtItems.GetAllDebtItemsForBudgeteer(budgeteer.BudgeteerId);
                var goalItems = _repo.GoalItems.GetAllGoalItemsForBudgeteer(budgeteer.BudgeteerId);
                var randomExpenses = _repo.RandomExpenses.GetAllRandomExpensesForBudget(budget.BudgetId);
                ViewModel budgeteerView = new ViewModel
                {
                    Budgeteer = budgeteer,
                    Budget = budget,
                    Wallet = wallet,
                    Bills = bills,
                    BudgetItems = budgetItems,
                    DebtItems = debtItems,
                    GoalItems = goalItems,
                    RandomExpenses = randomExpenses
                };
                return View(budgeteerView);
            }
            else
            {
                return RedirectToAction("CreateBudgeteer");
            }
        }

        public Budget GetCheckAndCreateBudget(Budgeteer budgeteer)
        {
            if(!(_repo.Budgets.FindByCondition(b => b.BudgeteerId == budgeteer.BudgeteerId).Any()))
            {
                var newBudget = new Budget
                {
                    BudgetStartDate = DateTime.Today,
                    MonthId = DateTime.Now.Month,
                    Year = DateTime.Now.Year,
                    BudgeteerId = budgeteer.BudgeteerId
                };
                _repo.Budgets.Create(newBudget);
                _repo.Save();
            }

            //Grab their current budget to pass to View
            var budget = _repo.Budgets.GetBudgetByBudgeteerIdMonthAndYear(budgeteer.BudgeteerId, DateTime.Now.Month, DateTime.Now.Year);

            if (budget == null)//If budget is null, that means there is a budget in the database, but its not current, need to make a new one
            {
                var lastMonthBudget = _repo.Budgets.GetBudgetByBudgeteerIdMonthAndYear(budgeteer.BudgeteerId, DateTime.Now.Month - 1, DateTime.Now.Year);
                budget = new Budget
                {
                    MonthlyIncome = lastMonthBudget.MonthlyIncome,
                    MonthlyLimit = lastMonthBudget.MonthlyLimit,
                    RandomExpenseLimit = lastMonthBudget.RandomExpenseLimit,
                    MonthlyBillMoney = 0,
                    MonthlyBudgetItemMoney = 0,
                    MonthlyDebtItemMoney = 0,
                    MonthlyGoalItemMoney = 0,
                    MonthlyMoneySaved = 0,
                    MonthlyRandomExpenseMoney = 0,
                    MonthlyTotalMoney = 0,
                    BudgetStartDate = DateTime.Today,
                    MonthId = DateTime.Now.Month,
                    Year = DateTime.Now.Year,
                    BudgeteerId = budgeteer.BudgeteerId
                };
                _repo.Budgets.Create(budget);
                _repo.Save();
            }
            return budget;
        }

        // GET: Budgeteer/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Budgeteer/Create
        public ActionResult CreateBudgeteer()
        {
            return View();
        }

        // POST: Budgeteer/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateBudgeteer(Budgeteer budgeteer)
        {
            try
            {
                var wallet = new Wallet();
                _repo.Wallets.Create(wallet);
                _repo.Save();

                var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
                budgeteer.UserId = userId;
                budgeteer.WalletId = _repo.Wallets.GetWallet(wallet.WalletId).WalletId;

                _repo.Budgeteers.Create(budgeteer);
                _repo.Save();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(budgeteer);
            }
        }

        //GET: Bill/Create
        public ActionResult CreateBill()
        {
            return View();
        }

        // POST: Bill/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateBill(Bill bill)
        {
            try
            {
                var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
                var budgeteer = _repo.Budgeteers.GetBudgeteerByUserId(userId);

                var newBill = new Bill
                {
                    BillName = bill.BillName,
                    BillType = bill.BillType,
                    DueDate = bill.DueDate,
                    Amount = bill.Amount,
                    BudgeteerId = budgeteer.BudgeteerId
                };
                _repo.Bills.Create(newBill);
                _repo.Save();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(bill);
            }
        }

        //GET: BudgetItem/Create
        public ActionResult CreateBudgetItem()
        {
            return View();
        }

        // POST: BudgetItem/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateBudgetItem(BudgetItem budgetItem)
        {
            try
            {
                var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
                var budgeteer = _repo.Budgeteers.GetBudgeteerByUserId(userId);

                var newBudgetItem = new BudgetItem
                {
                    BudgetItemName = budgetItem.BudgetItemName,
                    Category = budgetItem.Category,
                    Amount = budgetItem.Amount,
                    BudgeteerId = budgeteer.BudgeteerId
                };
                _repo.BudgetItems.Create(newBudgetItem);
                _repo.Save();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(budgetItem);
            }
        }

        //GET: DebtItem/Create
        public ActionResult CreateDebtItem()
        {
            return View();
        }

        // POST: DebtItem/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateDebtItem(DebtItem debtItem)
        {
            try
            {
                var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
                var budgeteer = _repo.Budgeteers.GetBudgeteerByUserId(userId);

                var newDebtItem = new DebtItem
                {
                    DebtItemName = debtItem.DebtItemName,
                    Category = debtItem.Category,
                    TotalDebtAmount = debtItem.TotalDebtAmount,
                    AmountToPayPerMonth = debtItem.AmountToPayPerMonth,
                    BudgeteerId = budgeteer.BudgeteerId
                };
                _repo.DebtItems.Create(newDebtItem);
                _repo.Save();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(debtItem);
            }
        }

        //GET: GoalItem/Create
        public ActionResult CreateGoalItem()
        {
            return View();
        }

        // POST: GoalItem/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateGoalItem(GoalItem goalItem)
        {
            try
            {
                var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
                var budgeteer = _repo.Budgeteers.GetBudgeteerByUserId(userId);

                var newGoalItem = new GoalItem
                {
                    GoalItemName = goalItem.GoalItemName,
                    Category = goalItem.Category,
                    Amount = goalItem.Amount,
                    BudgeteerId = budgeteer.BudgeteerId
                };
                _repo.GoalItems.Create(newGoalItem);
                _repo.Save();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(goalItem);
            }
        }

        //GET: RandomExpens/Create
        public ActionResult CreateRandomExpense()
        {
            return View();
        }

        // POST: RandomExpens/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateRandomExpens(RandomExpense randomExpense)
        {
            try
            {
                var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
                var budgeteer = _repo.Budgeteers.GetBudgeteerByUserId(userId);
                var budget = _repo.Budgets.GetBudgetByBudgeteerIdMonthAndYear(budgeteer.BudgeteerId, DateTime.Now.Month, DateTime.Now.Year);

                var newRandomExpense = new RandomExpense
                {
                    RandomExpenseName = randomExpense.RandomExpenseName,
                    Category = randomExpense.Category,
                    Amount = randomExpense.Amount,
                    BudgetId = budget.BudgetId
                };
                _repo.RandomExpenses.Create(newRandomExpense);
                _repo.Save();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(randomExpense);
            }
        }

        // GET: Budgeteer/Edit/5
        public ActionResult EditBudgeteer(int id)
        {
            var budgeteerFromDb = _repo.Budgeteers.GetBudgeteer(id);
            return View(budgeteerFromDb);
        }

        // POST: Budgeteer/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditBudgeteer(int id, Budgeteer budgeteer)
        {
            try
            {
                var budgeteerFromDb = _repo.Budgeteers.GetBudgeteer(id);
                budgeteerFromDb.FirstName = budgeteer.FirstName;
                budgeteerFromDb.LastName = budgeteer.LastName;
                budgeteerFromDb.Email = budgeteer.Email;
                budgeteerFromDb.PhoneNumber = budgeteer.PhoneNumber;
                _repo.Budgeteers.Update(budgeteerFromDb);
                _repo.Save();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(budgeteer);
            }
        }

        // GET: Budget/Edit/5
        public ActionResult EditBudget(int budgetId)
        {
            var budgetFromDb = _repo.Budgets.GetBudget(budgetId);
            return View(budgetFromDb);
        }

        // POST: Budget/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditBudget(int budgetId, Budget budget)
        {
            try
            {
                var budgetFromDb = _repo.Budgets.GetBudget(budgetId);
                budgetFromDb.MonthlyIncome = budget.MonthlyIncome;
                budgetFromDb.MonthlyLimit = budget.MonthlyLimit;
                budgetFromDb.RandomExpenseLimit = budget.RandomExpenseLimit;
                _repo.Budgets.Update(budgetFromDb);
                _repo.Save();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(budget);
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository.Contracts;
using Repository.Models;
using SoarBudgetV2.Contracts;
using SoarBudgetV2.Models;

namespace SoarBudgetV2.Controllers
{
    public class BudgeteerController : Controller
    {
        private readonly IRepositoryWrapper _repo;
        private readonly ISmsServices _smsService;

        public BudgeteerController(IRepositoryWrapper repo, ISmsServices smsServices)
        {
            _repo = repo;
            _smsService = smsServices;
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
                var bills = _repo.Bills.GetAllBillsForBudgeteer(budgeteer.BudgeteerId).Where(b => b.IsPaid == false && b.DueDate.Month == DateTime.Now.Month).ToList();
                var debtItems = _repo.DebtItems.GetAllDebtItemsForBudgeteer(budgeteer.BudgeteerId).Where(d => d.IsPaid == false && d.DueDate.Month == DateTime.Now.Month).ToList();
                var budgetItems = _repo.BudgetItems.GetAllBudgetItemsForBudgeteer(budgeteer.BudgeteerId);
                var budgetItemExpenses = _repo.BudgetItemExpenses.GetAllBudgetItemExpensesForBudget(budget.BudgetId);
                var randomExpenses = _repo.RandomExpenses.GetAllRandomExpensesForBudget(budget.BudgetId);
                var goalItems = _repo.GoalItems.GetAllGoalItemsForBudgeteer(budgeteer.BudgeteerId);
                var upcomingBills = CheckForDueBillsAndDebts(bills, debtItems);
                var lateBills = CheckForLateBillsAndDebts(bills, debtItems);
                var approachingAlerts = CheckForApproachingSpendingLimit(budget);
                var overspendingAlerts = CheckForOverSpending(budget);
                //if(upcomingBills.Count > 0)
                //{
                //    _smsService.SendSMS(budgeteer, upcomingBills);
                //}

                ViewModel budgeteerView = new ViewModel
                {
                    Budgeteer = budgeteer,
                    Budget = budget,
                    Wallet = wallet,
                    Bills = bills,
                    BudgetItems = budgetItems,
                    DebtItems = debtItems,
                    GoalItems = goalItems,
                    RandomExpenses = randomExpenses,
                    BudgetItemExpenses = budgetItemExpenses,
                    UpcomingBills = upcomingBills,
                    LateBills = lateBills,
                    ApproachingAlerts = approachingAlerts,
                    OverspendingAlerts = overspendingAlerts
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
                //grabs the last months budget so that important details can be transferred over and saved money can be calculated
                var lastMonthBudget = _repo.Budgets.GetBudgetByBudgeteerIdMonthAndYear(budgeteer.BudgeteerId, DateTime.Now.Month - 1, DateTime.Now.Year);
                CalculateMoneySaved(lastMonthBudget, budgeteer);
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
                    CoffeeCategorySpent = 0,
                    DiningOutCategorySpent = 0,
                    EntertainmentCategorySpent = 0,
                    GasCategorySpent = 0,
                    GroceriesCategorySpent = 0,
                    CoffeeCategoryLimit = lastMonthBudget.CoffeeCategoryLimit,
                    DiningOutCategoryLimit = lastMonthBudget.DiningOutCategoryLimit,
                    EntertainmentCategoryLimit = lastMonthBudget.EntertainmentCategoryLimit,
                    GasCategoryLimit = lastMonthBudget.GasCategoryLimit,
                    GroceriesCategoryLimit = lastMonthBudget.GroceriesCategoryLimit,
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
                var budget = _repo.Budgets.GetBudgetByBudgeteerIdMonthAndYear(budgeteer.BudgeteerId, DateTime.Now.Month, DateTime.Now.Year);

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

                budget.MonthlyLimit += newBill.Amount;
                _repo.Budgets.Update(budget);
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
                var budget = _repo.Budgets.GetBudgetByBudgeteerIdMonthAndYear(budgeteer.BudgeteerId, DateTime.Now.Month, DateTime.Now.Year);

                var newBudgetItem = new BudgetItem
                {
                    BudgetItemName = budgetItem.BudgetItemName,
                    Category = budgetItem.Category,
                    Amount = budgetItem.Amount,
                    BudgeteerId = budgeteer.BudgeteerId
                };
                _repo.BudgetItems.Create(newBudgetItem);
                _repo.Save();

                ConfigureCategoryLimits(newBudgetItem, budget);

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
                var budget = _repo.Budgets.GetBudgetByBudgeteerIdMonthAndYear(budgeteer.BudgeteerId, DateTime.Now.Month, DateTime.Now.Year);

                var newDebtItem = new DebtItem
                {
                    DebtItemName = debtItem.DebtItemName,
                    Category = debtItem.Category,
                    TotalDebtAmount = debtItem.TotalDebtAmount,
                    AmountToPayPerMonth = debtItem.AmountToPayPerMonth,
                    DueDate = debtItem.DueDate,
                    BudgeteerId = budgeteer.BudgeteerId
                };
                _repo.DebtItems.Create(newDebtItem);
                _repo.Save();

                budget.MonthlyLimit += newDebtItem.AmountToPayPerMonth;
                _repo.Budgets.Update(budget);
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
        public ActionResult CreateRandomExpense(RandomExpense randomExpense)
        {
            try
            {
                var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
                var budgeteer = _repo.Budgeteers.GetBudgeteerByUserId(userId);
                var budget = _repo.Budgets.GetBudgetByBudgeteerIdMonthAndYear(budgeteer.BudgeteerId, DateTime.Now.Month, DateTime.Now.Year);
                var wallet = _repo.Wallets.GetWallet(budgeteer.WalletId);

                var newRandomExpense = new RandomExpense
                {
                    RandomExpenseName = randomExpense.RandomExpenseName,
                    Category = randomExpense.Category,
                    Amount = randomExpense.Amount,
                    BudgetId = budget.BudgetId
                };
                _repo.RandomExpenses.Create(newRandomExpense);
                _repo.Save();

                budget.MonthlyRandomExpenseMoney += randomExpense.Amount;
                budget.MonthlyTotalMoney += randomExpense.Amount;
                _repo.Budgets.Update(budget);
                _repo.Save();

                wallet.TotalRandomExpenseMoney += randomExpense.Amount;
                wallet.TotalMoney += randomExpense.Amount;
                _repo.Wallets.Update(wallet);
                _repo.Save();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(randomExpense);
            }
        }

        //GET: BudgetItemExpense/Create
        public ActionResult CreateBudgetItemExpense()
        {
            return View();
        }

        // POST: BudgetItemExpense/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateBudgetItemExpense(BudgetItemExpense budgetItemExpense)
        {
            try
            {
                var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
                var budgeteer = _repo.Budgeteers.GetBudgeteerByUserId(userId);
                var budget = _repo.Budgets.GetBudgetByBudgeteerIdMonthAndYear(budgeteer.BudgeteerId, DateTime.Now.Month, DateTime.Now.Year);
                var wallet = _repo.Wallets.GetWallet(budgeteer.WalletId);

                var newBudgetItemExpense = new BudgetItemExpense
                {
                    BudgetItemExpenseName = budgetItemExpense.BudgetItemExpenseName,
                    Category = budgetItemExpense.Category,
                    Amount = budgetItemExpense.Amount,
                    BudgetId = budget.BudgetId
                };
                _repo.BudgetItemExpenses.Create(newBudgetItemExpense);
                _repo.Save();

                budget.MonthlyBudgetItemMoney += budgetItemExpense.Amount;
                budget.MonthlyTotalMoney += budgetItemExpense.Amount;
                _repo.Budgets.Update(budget);
                _repo.Save();

                wallet.TotalBudgetItemMoney += budgetItemExpense.Amount;
                wallet.TotalMoney += budgetItemExpense.Amount;
                _repo.Wallets.Update(wallet);
                _repo.Save();

                ConfigureCategorySpending(newBudgetItemExpense, budget);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(budgetItemExpense);
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
                budgetFromDb.MonthlyLimit -= budgetFromDb.RandomExpenseLimit;
                budgetFromDb.RandomExpenseLimit = budget.RandomExpenseLimit;
                budgetFromDb.MonthlyLimit += budget.RandomExpenseLimit;
                _repo.Budgets.Update(budgetFromDb);
                _repo.Save();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(budget);
            }
        }

        public ActionResult PayBill(int billId)
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var budgeteer = _repo.Budgeteers.GetBudgeteerByUserId(userId);

            var billFromDb = _repo.Bills.GetBill(billId);
            billFromDb.IsPaid = true;
            _repo.Bills.Update(billFromDb);
            _repo.Save();

            var budget = _repo.Budgets.GetBudgetByBudgeteerIdMonthAndYear(budgeteer.BudgeteerId, DateTime.Now.Month, DateTime.Now.Year);
            budget.MonthlyBillMoney += billFromDb.Amount;
            budget.MonthlyTotalMoney += billFromDb.Amount;
            _repo.Budgets.Update(budget);
            _repo.Save();

            var wallet = _repo.Wallets.GetWallet(budgeteer.WalletId);
            wallet.TotalBillMoney += billFromDb.Amount;
            wallet.TotalMoney += billFromDb.Amount;
            _repo.Wallets.Update(wallet);
            _repo.Save();

            var nextMonthBill = new Bill
            {
                BillName = billFromDb.BillName,
                BillType = billFromDb.BillType,
                Amount = billFromDb.Amount,
                DueDate = billFromDb.DueDate.AddMonths(1),
                BudgeteerId = billFromDb.BudgeteerId
            };
            _repo.Bills.Create(nextMonthBill);
            _repo.Save();
            return RedirectToAction("Index");
        }

        public ActionResult PayDebtItem(int debtItemId)
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var budgeteer = _repo.Budgeteers.GetBudgeteerByUserId(userId);

            var debtItemFromDb = _repo.DebtItems.GetDebtItem(debtItemId);
            debtItemFromDb.IsPaid = true;
            _repo.DebtItems.Update(debtItemFromDb);
            _repo.Save();

            var budget = _repo.Budgets.GetBudgetByBudgeteerIdMonthAndYear(budgeteer.BudgeteerId, DateTime.Now.Month, DateTime.Now.Year);
            budget.MonthlyDebtItemMoney += debtItemFromDb.AmountToPayPerMonth;
            budget.MonthlyTotalMoney += debtItemFromDb.AmountToPayPerMonth;
            _repo.Budgets.Update(budget);
            _repo.Save();

            var wallet = _repo.Wallets.GetWallet(budgeteer.WalletId);
            wallet.TotalDebtItemMoney += debtItemFromDb.AmountToPayPerMonth;
            wallet.TotalMoney += debtItemFromDb.AmountToPayPerMonth;
            _repo.Wallets.Update(wallet);
            _repo.Save();

            var nextMonthDebtItem = new DebtItem
            {
                DebtItemName = debtItemFromDb.DebtItemName,
                Category = debtItemFromDb.Category,
                AmountToPayPerMonth = debtItemFromDb.AmountToPayPerMonth,
                TotalDebtAmount = debtItemFromDb.TotalDebtAmount -= debtItemFromDb.AmountToPayPerMonth,
                DueDate = debtItemFromDb.DueDate.AddMonths(1),
                BudgeteerId = debtItemFromDb.BudgeteerId
            };
            _repo.DebtItems.Create(nextMonthDebtItem);
            _repo.Save();
            return RedirectToAction("Index");
        }

        public ActionResult PurchaseGoalItem(int goalItemId)
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var budgeteer = _repo.Budgeteers.GetBudgeteerByUserId(userId);
            var wallet = _repo.Wallets.GetWallet(budgeteer.WalletId);
            var goalItemFromDb = _repo.GoalItems.GetGoalItem(goalItemId);
            if(wallet.ExtraCash >= goalItemFromDb.Amount)
            {
                wallet.ExtraCash -= goalItemFromDb.Amount;
                wallet.TotalGoalItemMoney += goalItemFromDb.Amount;
                wallet.TotalMoney += goalItemFromDb.Amount;

                _repo.Wallets.Update(wallet);
                _repo.Save();

                _repo.GoalItems.Delete(goalItemFromDb);
                _repo.Save();
            }
            return RedirectToAction("Index");
        }

        public void ConfigureCategoryLimits(BudgetItem budgetItem, Budget budget)
        {
            switch (budgetItem.Category)
            {
                case ("Coffee"):
                    budget.CoffeeCategoryLimit += budgetItem.Amount;
                    break;
                case ("Dining Out"):
                    budget.DiningOutCategoryLimit += budgetItem.Amount;
                    break;
                case ("Entertainment"):
                    budget.EntertainmentCategoryLimit += budgetItem.Amount;
                    break;
                case ("Gas"):
                    budget.GasCategoryLimit += budgetItem.Amount;
                    break;
                case ("Groceries"):
                    budget.GroceriesCategoryLimit += budgetItem.Amount;
                    break;
            }
            budget.MonthlyLimit += budgetItem.Amount;
            _repo.Budgets.Update(budget);
            _repo.Save();
        }

        public void ConfigureCategorySpending(BudgetItemExpense budgetItemExpense, Budget budget)
        {
            switch (budgetItemExpense.Category)
            {
                case ("Coffee"):
                    budget.CoffeeCategorySpent += budgetItemExpense.Amount;
                    break;
                case ("Dining Out"):
                    budget.DiningOutCategorySpent += budgetItemExpense.Amount;
                    break;
                case ("Entertainment"):
                    budget.EntertainmentCategorySpent += budgetItemExpense.Amount;
                    break;
                case ("Gas"):
                    budget.GasCategorySpent += budgetItemExpense.Amount;
                    break;
                case ("Groceries"):
                    budget.GroceriesCategorySpent += budgetItemExpense.Amount;
                    break;
            }
            _repo.Budgets.Update(budget);
            _repo.Save();
        }

        public List<string> CheckForDueBillsAndDebts(List<Bill> bills, List<DebtItem> debtItems)
        {
            List<string> dueBillsAndDebtsAlerts = new List<string>();
            foreach(var bill in bills)
            {
                var daysUntilBillDue = bill.DueDate.Day - DateTime.Now.Day;
                if (daysUntilBillDue <= 3 && daysUntilBillDue >= 0)
                {
                    dueBillsAndDebtsAlerts.Add($"{bill.BillName} is due soon!");
                }
            }
            foreach(var debtItem in debtItems)
            {
                var daysUntilDebtDue = debtItem.DueDate.Day - DateTime.Now.Day;
                if (daysUntilDebtDue <= 3 && daysUntilDebtDue >= 0)
                {
                    dueBillsAndDebtsAlerts.Add($"{debtItem.DebtItemName} is due soon!");
                }
            }
            return dueBillsAndDebtsAlerts;
        }

        public List<string> CheckForLateBillsAndDebts(List<Bill> bills, List<DebtItem> debtItems)
        {
            List<string> lateBillsAndDebtsAlerts = new List<string>();
            foreach (var bill in bills)
            {
                var daysLate = bill.DueDate.Day - DateTime.Now.Day;
                if (daysLate < 0)
                {
                    lateBillsAndDebtsAlerts.Add($"{bill.BillName} is late!");
                }
            }
            foreach (var debtItem in debtItems)
            {
                var daysLate = debtItem.DueDate.Day - DateTime.Now.Day;
                if (daysLate < 0)
                {
                    lateBillsAndDebtsAlerts.Add($"{debtItem.DebtItemName} is late!");
                }
            }
            return lateBillsAndDebtsAlerts;
        }

        public void CalculateMoneySaved(Budget budget, Budgeteer budgeteer)
        {
            var moneySaved = budget.MonthlyIncome - budget.MonthlyTotalMoney;
            var moneyUnderBudget = budget.MonthlyLimit - budget.MonthlyTotalMoney;
            var wallet = _repo.Wallets.GetWallet(budgeteer.WalletId);

            if(moneySaved > 0)
            {
                wallet.TotalMoneySaved = moneySaved;
            }
            if(moneyUnderBudget > 0)
            {
                wallet.ExtraCash = moneyUnderBudget;
            }
            _repo.Wallets.Update(wallet);
            _repo.Save();
        }

        public List<string> CheckForOverSpending(Budget budget)
        {
            List<string> overspendingAlerts = new List<string>();
            if(budget.CoffeeCategorySpent > budget.CoffeeCategoryLimit)
            {
                overspendingAlerts.Add($"You're over your coffee limit!");
            }
            if (budget.EntertainmentCategorySpent > budget.EntertainmentCategoryLimit)
            {
                overspendingAlerts.Add($"You're over your entertainment limit!");
            }
            if (budget.GasCategorySpent > budget.GasCategoryLimit)
            {
                overspendingAlerts.Add($"You're over your gas limit!");
            }
            if (budget.GroceriesCategorySpent > budget.GroceriesCategoryLimit)
            {
                overspendingAlerts.Add($"You're over your groceries limit!");
            }
            if (budget.DiningOutCategorySpent > budget.DiningOutCategoryLimit)
            {
                overspendingAlerts.Add($"You're over your dining limit!");
            }
            if (budget.MonthlyRandomExpenseMoney > budget.RandomExpenseLimit)
            {
                overspendingAlerts.Add($"You're over you're random expense limit!");
            }
            if (budget.MonthlyTotalMoney > budget.MonthlyLimit)
            {
                overspendingAlerts.Add($"You're over your budget limit for the month!");
            }
            return overspendingAlerts;
        }

        public List<string> CheckForApproachingSpendingLimit(Budget budget)
        {
            double percentageOfLimit = 0.7;
            List<string> approachingLimitAlerts = new List<string>();
            if (budget.CoffeeCategorySpent > (budget.CoffeeCategoryLimit * percentageOfLimit) && budget.CoffeeCategorySpent < budget.CoffeeCategoryLimit)
            {
                approachingLimitAlerts.Add($"You're getting close to your coffee limit!");
            }
            if (budget.EntertainmentCategorySpent > (budget.EntertainmentCategoryLimit * percentageOfLimit) && budget.EntertainmentCategorySpent < budget.EntertainmentCategoryLimit)
            {
                approachingLimitAlerts.Add($"You're getting close to your entertainment limit!");
            }
            if (budget.GasCategorySpent > (budget.GasCategoryLimit * percentageOfLimit) && budget.GasCategorySpent < budget.GasCategoryLimit)
            {
                approachingLimitAlerts.Add($"You're getting close to your gas limit!");
            }
            if (budget.GroceriesCategorySpent > (budget.GroceriesCategoryLimit * percentageOfLimit) && budget.GroceriesCategorySpent < budget.GroceriesCategoryLimit)
            {
                approachingLimitAlerts.Add($"You're getting close to your groceries limit!");
            }
            if (budget.DiningOutCategorySpent > (budget.DiningOutCategoryLimit * percentageOfLimit) && budget.DiningOutCategorySpent < budget.DiningOutCategoryLimit)
            {
                approachingLimitAlerts.Add($"You're getting close to your dining limit!");
            }
            if (budget.MonthlyRandomExpenseMoney > (budget.RandomExpenseLimit * percentageOfLimit) && budget.MonthlyRandomExpenseMoney < budget.RandomExpenseLimit)
            {
                approachingLimitAlerts.Add($"You're getting close to your random expense limit!");
            }
            if (budget.MonthlyTotalMoney > (budget.MonthlyLimit * percentageOfLimit) && budget.MonthlyTotalMoney < budget.MonthlyLimit)
            {
                approachingLimitAlerts.Add($"You're getting close to your budget limit!");
            }
            return approachingLimitAlerts;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository.Contracts;
using Repository.Models;

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
            if (_repo.Budgeteers.FindByCondition(e => e.UserId == userId).Any())
            {
                var budgeteer = _repo.Budgeteers.FindByCondition(e => e.UserId == userId).FirstOrDefault();

                return View(budgeteer);
            }
            else
            {
                return RedirectToAction("Create");
            }
        }

        // GET: Budgeteer/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Budgeteer/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Budgeteer/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Budgeteer budgeteer)
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

        // GET: Budgeteer/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Budgeteer/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Budgeteer/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Budgeteer/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
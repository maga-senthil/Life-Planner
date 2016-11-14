using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FamilyManagement.Models;
using System.Globalization;

namespace FamilyManagement.Controllers
{
    public class DailyExpensesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: DailyExpenses
        public ActionResult Index()
        {
            var dailyExpense = db.dailyExpense.Include(d => d.ItemCategorys);
            return View(dailyExpense.ToList());
        }

        // GET: DailyExpenses/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DailyExpense dailyExpense = db.dailyExpense.Find(id);
            if (dailyExpense == null)
            {
                return HttpNotFound();
            }
            return View(dailyExpense);
        }

        // GET: DailyExpenses/Create
        public ActionResult Create()
        {
            ViewBag.ItemCategoryId = new SelectList(db.itemCategory, "Id", "ItemName");
            return View();
        }

        // POST: DailyExpenses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Day,ItemCategoryId,Amount")] DailyExpense dailyExpense)
        {
            if (ModelState.IsValid)
            {
                db.dailyExpense.Add(dailyExpense);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ItemCategoryId = new SelectList(db.itemCategory, "Id", "ItemName", dailyExpense.ItemCategoryId);
            return View(dailyExpense);
        }

        // GET: DailyExpenses/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DailyExpense dailyExpense = db.dailyExpense.Find(id);
            if (dailyExpense == null)
            {
                return HttpNotFound();
            }
            ViewBag.ItemCategoryId = new SelectList(db.itemCategory, "Id", "ItemName", dailyExpense.ItemCategoryId);
            return View(dailyExpense);
        }

        // POST: DailyExpenses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Day,ItemCategoryId,Amount")] DailyExpense dailyExpense)
        {
            if (ModelState.IsValid)
            {
                db.Entry(dailyExpense).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ItemCategoryId = new SelectList(db.itemCategory, "Id", "ItemName", dailyExpense.ItemCategoryId);
            return View(dailyExpense);
        }

        // GET: DailyExpenses/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DailyExpense dailyExpense = db.dailyExpense.Find(id);
            if (dailyExpense == null)
            {
                return HttpNotFound();
            }
            return View(dailyExpense);
        }

        // POST: DailyExpenses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DailyExpense dailyExpense = db.dailyExpense.Find(id);
            db.dailyExpense.Remove(dailyExpense);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public ActionResult DailyExpenseView()
        {
            DailyExpenseViewModel model = new DailyExpenseViewModel();
            ViewBag.ItemCategoryId = new SelectList(db.itemCategory, "Id", "ItemName", model.ItemCategoryId);
            return View(model);
        }
        [HttpPost]
        public ActionResult DailyExpenseView(DailyExpenseViewModel model,DateTime Day)
        {
            var k = Day.Month;
            var month = DateTimeFormatInfo.CurrentInfo.GetMonthName(k);
            model.budgetdetail = db.monthlyBudget.Where(b => b.BudgetMonth == month).ToList();
           // model.dailyexpensedetail = db.dailyExpense.Where(b => b.Day.Month == Day.Month).ToList();
            //model.budgetdetail = db.monthlyBudget;
            // model.budgetdetail = db.monthlyBudget.Where(s => s.BudgetMonth == BudgetMonth).ToList() ;
            //model.dailyexpensedetail.Reverse();
            ItemCategory newItem = new ItemCategory();
            newItem = db.itemCategory.FirstOrDefault(x => x.Id == model.ItemCategoryId);
            var matchcCategory = newItem.CategoryId;
            MonthlyBudget newBudget = new MonthlyBudget();
            newBudget = db.monthlyBudget.FirstOrDefault(y => y.CategoryId == matchcCategory && y.BudgetMonth == month);
            var s = DateTime.ParseExact(newBudget.BudgetMonth, "MMMM", System.Globalization.CultureInfo.InvariantCulture).Month;
            if (model.Day.Month == s)
            {
                newBudget.ActualAmount += model.Amount;
                newBudget.Difference = newBudget.BudgetAmount - newBudget.ActualAmount;
            }
            //MonthlyBudget budget1 = db.monthlyBudget.FirstOrDefault(s => s.BudgetMonth.Month == BudgetMonth.Month);
            //model.dailyexpensedetail = db.dailyExpense;
            ////model.dailyexpensedetail.Reverse();
            //model.budgetdetail = db.monthlyBudget;
            //ItemCategory newItem = new ItemCategory();
            //newItem = db.itemCategory.FirstOrDefault(x => x.Id == model.ItemCategoryId);
            //var matchcCategory = newItem.CategoryId;
            //MonthlyBudget newBudget = new MonthlyBudget();
            //newBudget = db.monthlyBudget.FirstOrDefault(y => y.CategoryId == matchcCategory);
            //if (model.Day.Month == newBudget.BudgetMonth.Month)
            //{
            //    newBudget.ActualAmount += model.Amount;
            //    newBudget.Difference = newBudget.BudgetAmount - newBudget.ActualAmount;
            //}
            Category newCategory = new Category();
            var categoryNameList = db.category.Select(a => a).ToList();
            foreach (var item in categoryNameList)
            {
                var categoryName = db.category.FirstOrDefault(z => z.Id == matchcCategory);
                model.CategoryName = categoryName.CategoryName;
            }
            model.BudgetAmount = newBudget.BudgetAmount;
            model.ActualAmount = newBudget.ActualAmount;
            // model.Difference = newBudget.Difference;
            model.BudgetMonth = newBudget.BudgetMonth;


            var budgetList = db.monthlyBudget.Select(b => b).ToList();
            foreach (var item in budgetList)
            {
                item.Difference = item.BudgetAmount - item.ActualAmount;
                model.Difference = item.Difference;
            }
            var budgettal1 = db.monthlyBudget.Where(a => a.BudgetMonth == month).Select (l=>l.BudgetAmount).ToList();
           // var budgetall = db.monthlyBudget.Select(a => a.BudgetAmount).ToList();
            for (var i = 0; i < budgettal1.Count; i++)
            {
                model.TotalBudget += budgettal1[i];
            }

            var newExpense = new DailyExpense
            {
                Day = model.Day,
                ItemCategoryId = model.ItemCategoryId,
                Amount = model.Amount
            };
            db.dailyExpense.Add(newExpense);
            db.SaveChanges();

            var budgetall1 = db.monthlyBudget.Where(a => a.BudgetMonth == month).Select(a => a.ActualAmount).ToList();
            for (var i = 0; i < budgetall1.Count; i++)
            {
                model.TotalActual += budgetall1[i];
            }

            model.TotalDifference = model.TotalBudget - model.TotalActual;

            model.dailyexpensedetail = db.dailyExpense.Where(b => b.Day.Month == Day.Month).ToList();

            ModelState.Clear();
            ViewBag.ItemCategoryId = new SelectList(db.itemCategory, "Id", "ItemName", model.ItemCategoryId);
            return View(model);
        }
        public ActionResult MonthlyChart()
        {
            return View();
        }
        public ActionResult ChartView()
        {
            return View();
        }
        public ActionResult MonthlyBudgetSearch()
        {
            DailyExpenseViewModel model = new DailyExpenseViewModel();
            return View(model);
        }
        [HttpPost]
        public ActionResult MonthlyBudgetSearch(DailyExpenseViewModel model,string Budgetmonth)
        {
            model.budgetdetail = db.monthlyBudget.Where(b => b.BudgetMonth == Budgetmonth).ToList();
            var detail = model.budgetdetail;

           foreach (var item in detail)
            {
                var name = db.category.FirstOrDefault(x => x.Id == item.CategoryId);
                model.CategoryName = name.CategoryName;
            }
            var detail1 = db.monthlyBudget.Where(b => b.BudgetMonth == Budgetmonth).Select(a => a.ActualAmount).ToList();
            for (var i = 0; i < detail1.Count; i++)
            {
                model.TotalActual += detail1[i];
            }
            var detail2 = db.monthlyBudget.Where(b => b.BudgetMonth == Budgetmonth).Select(a => a.BudgetAmount).ToList();
            for (var i = 0; i < detail2.Count; i++)
            {
                model.TotalBudget += detail2[i];
            }
            model.TotalDifference = model.TotalBudget - model.TotalActual;

            return View(model);
        }

    }
}

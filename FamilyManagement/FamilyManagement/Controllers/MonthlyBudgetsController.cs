using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FamilyManagement.Models;

namespace FamilyManagement.Controllers
{
    public class MonthlyBudgetsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: MonthlyBudgets
        public ActionResult Index()
        {
            var monthlyBudget = db.monthlyBudget.Include(m => m.Categorys);
            return View(monthlyBudget.ToList());
        }

        // GET: MonthlyBudgets/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MonthlyBudget monthlyBudget = db.monthlyBudget.Find(id);
            if (monthlyBudget == null)
            {
                return HttpNotFound();
            }
            return View(monthlyBudget);
        }

        // GET: MonthlyBudgets/Create
        public ActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(db.category, "Id", "CategoryName");
            return View();
        }

        // POST: MonthlyBudgets/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,CategoryId,BudgetMonth,BudgetAmount,ActualAmount,Difference")] MonthlyBudget monthlyBudget)
        {
            if (ModelState.IsValid)
            {
                db.monthlyBudget.Add(monthlyBudget);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CategoryId = new SelectList(db.category, "Id", "CategoryName", monthlyBudget.CategoryId);
            return View(monthlyBudget);
        }

        // GET: MonthlyBudgets/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MonthlyBudget monthlyBudget = db.monthlyBudget.Find(id);
            if (monthlyBudget == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryId = new SelectList(db.category, "Id", "CategoryName", monthlyBudget.CategoryId);
            return View(monthlyBudget);
        }

        // POST: MonthlyBudgets/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,CategoryId,BudgetMonth,BudgetAmount,ActualAmount,Difference")] MonthlyBudget monthlyBudget)
        {
            if (ModelState.IsValid)
            {
                db.Entry(monthlyBudget).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("MonthlyBudgetView");
            }
            ViewBag.CategoryId = new SelectList(db.category, "Id", "CategoryName", monthlyBudget.CategoryId);
            return View(monthlyBudget);
        }

        // GET: MonthlyBudgets/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MonthlyBudget monthlyBudget = db.monthlyBudget.Find(id);
            if (monthlyBudget == null)
            {
                return HttpNotFound();
            }
            return View(monthlyBudget);
        }

        // POST: MonthlyBudgets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MonthlyBudget monthlyBudget = db.monthlyBudget.Find(id);
            db.monthlyBudget.Remove(monthlyBudget);
            db.SaveChanges();
            return RedirectToAction("MonthlyBudgetView");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        public ActionResult MonthlyBudgetView()
        {
            MonthlyBudgetViewModel model = new MonthlyBudgetViewModel();
            //model.CategoryList = db.category;
            //model.BudgetList = db.monthlyBudget;
            ViewBag.CategoryId = new SelectList(db.category, "Id", "CategoryName", model.CategoryId);
            return View(model);
        }
        [HttpPost]
        public ActionResult MonthlyBudgetView(MonthlyBudgetViewModel model,string BudgetMonth)
        {
            model.CategoryList = db.category;

            var newMonthlyBuget = new MonthlyBudget
            {
                CategoryId = model.CategoryId,
                BudgetMonth = model.BudgetMonth,
                BudgetAmount = model.BudgetAmount,
                ActualAmount = 0,
                Difference = 0
            };
            db.monthlyBudget.Add(newMonthlyBuget);
            db.SaveChanges();
            model.BudgetList = db.monthlyBudget.Where(x => x.BudgetMonth == BudgetMonth).ToList();

            ViewBag.CategoryId = new SelectList(db.category, "Id", "CategoryName", model.CategoryId);
            return View(model);
        }

      

    }
}

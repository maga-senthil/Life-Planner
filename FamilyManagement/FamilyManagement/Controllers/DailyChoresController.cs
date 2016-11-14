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
    public class DailyChoresController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: DailyChores
        public ActionResult Index()
        {
            var dailyChore = db.dailyChore.Include(d => d.Chore).Include(d => d.Family);
            return View(dailyChore.ToList());
        }

        // GET: DailyChores/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DailyChore dailyChore = db.dailyChore.Find(id);
            if (dailyChore == null)
            {
                return HttpNotFound();
            }
            return View(dailyChore);
        }

        // GET: DailyChores/Create
        public ActionResult Create()
        {
            ViewBag.ChoreId = new SelectList(db.chore, "Id", "Chores");
            ViewBag.FamilyId = new SelectList(db.family, "Id", "Name");
            return View();
        }

        // POST: DailyChores/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,FamilyId,ChoreId,ChoreDay,Done")] DailyChore dailyChore)
        {
            if (ModelState.IsValid)
            {
                db.dailyChore.Add(dailyChore);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ChoreId = new SelectList(db.chore, "Id", "Chores", dailyChore.ChoreId);
            ViewBag.FamilyId = new SelectList(db.family, "Id", "Name", dailyChore.FamilyId);
            return View(dailyChore);
        }

        // GET: DailyChores/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DailyChore dailyChore = db.dailyChore.Find(id);
            if (dailyChore == null)
            {
                return HttpNotFound();
            }
            ViewBag.ChoreId = new SelectList(db.chore, "Id", "Chores", dailyChore.ChoreId);
            ViewBag.FamilyId = new SelectList(db.family, "Id", "Name", dailyChore.FamilyId);
            return View(dailyChore);
        }

        // POST: DailyChores/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,FamilyId,ChoreId,ChoreDay,Done")] DailyChore dailyChore)
        {
            if (ModelState.IsValid)
            {
                db.Entry(dailyChore).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ChoreId = new SelectList(db.chore, "Id", "Chores", dailyChore.ChoreId);
            ViewBag.FamilyId = new SelectList(db.family, "Id", "Name", dailyChore.FamilyId);
            return View(dailyChore);
        }

        // GET: DailyChores/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DailyChore dailyChore = db.dailyChore.Find(id);
            if (dailyChore == null)
            {
                return HttpNotFound();
            }
            return View(dailyChore);
        }

        // POST: DailyChores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DailyChore dailyChore = db.dailyChore.Find(id);
            db.dailyChore.Remove(dailyChore);
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
        public ActionResult DailyChoreView()
        {
            DailyChoreViewModel model = new DailyChoreViewModel();
            ViewBag.ChoreId = new SelectList(db.chore, "Id", "Chores", model.ChoreId);
            ViewBag.FamilyId = new SelectList(db.family, "Id", "Name", model.FamilyId);
            return View(model);
        }
        [HttpPost]
        public ActionResult DailyChoreView(DailyChoreViewModel model)
        {
            model.choredetail = db.chore;
            model.familydetail = db.family;
            // model.dailychoredetail = db.dailyChore.Where(z => z.FamilyId == model.FamilyId).ToList();
            Family familymember = new Family();
            familymember = db.family.FirstOrDefault(x => x.Id == model.FamilyId);
            model.Name = familymember.Name;
            Chore chorename = new Chore();
            chorename = db.chore.FirstOrDefault(y => y.Id == model.ChoreId);
            model.Chores = chorename.Chores;
            if (model.Done == true)
            {
                familymember.Amount += 1.50;
                familymember.Points += 1;
            }
            model.Points = familymember.Points;
            model.Amount = familymember.Amount;
            var dailyChore = new DailyChore
            {
                FamilyId = model.FamilyId,
                ChoreId = model.ChoreId,
                ChoreDay = model.ChoreDay,
                Done = model.Done
            };
            db.dailyChore.Add(dailyChore);
            db.SaveChanges();

            model.dailychoredetail = db.dailyChore.Where(z => z.FamilyId == model.FamilyId).ToList();

            ViewBag.ChoreId = new SelectList(db.chore, "Id", "Chores", model.ChoreId);
            ViewBag.FamilyId = new SelectList(db.family, "Id", "Name", model.FamilyId);
            return View(model);
        }
        public ActionResult SearchChore()
        {
            DailyChoreViewModel model = new DailyChoreViewModel();
            return View(model);
        }
        [HttpPost]
        public ActionResult SearchChore(DailyChoreViewModel model, String Name)
        {

            var name = db.family.FirstOrDefault(x => x.Name == Name);
            model.FamilyId = name.Id;
            model.Amount = name.Amount;
            model.Points = name.Points;
            model.dailychoredetail = db.dailyChore.Where(y => y.FamilyId == name.Id).ToList();
            var detail = model.dailychoredetail;
            foreach (var item in detail)
            {
                var chorename = db.chore.FirstOrDefault(z => z.Id == item.ChoreId);
                model.Chores = chorename.Chores;
            }
            return View("result", model);
        }
    }
}

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
    public class MaintanancesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Maintanances
        public ActionResult Index()
        {
            return View(db.maintanance.ToList());
        }

        // GET: Maintanances/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Maintanance maintanance = db.maintanance.Find(id);
            if (maintanance == null)
            {
                return HttpNotFound();
            }
            return View(maintanance);
        }

        // GET: Maintanances/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Maintanances/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Item,LastMaintenance,MaintenanceFrequency,NextMaintenance,MaintenanceCost")] Maintanance maintanance)
        {
            maintanance.NextMaintenance = AddSmartMonths(maintanance.LastMaintenance, maintanance.MaintenanceFrequency);

            if (ModelState.IsValid)
            {
                db.maintanance.Add(maintanance);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(maintanance);
        }

        public static DateTime AddSmartMonths(DateTime d, int nMonths)
        {
            int year = d.Year;
            int month = d.Month;
            int day = d.Day;

            if ((day == 30) && (day < DateTime.DaysInMonth(year, month)))
                d = d.AddDays(1);
            else if ((month == 1) && (day > 28))
                d = new DateTime(year, month, 31);

            return d.AddMonths(nMonths);
        }

        // GET: Maintanances/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Maintanance maintanance = db.maintanance.Find(id);
            if (maintanance == null)
            {
                return HttpNotFound();
            }
            return View(maintanance);
        }

        // POST: Maintanances/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Item,LastMaintenance,MaintenanceFrequency,NextMaintenance,MaintenanceCost")] Maintanance maintanance)
        {
            if (ModelState.IsValid)
            {
                db.Entry(maintanance).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(maintanance);
        }

        // GET: Maintanances/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Maintanance maintanance = db.maintanance.Find(id);
            if (maintanance == null)
            {
                return HttpNotFound();
            }
            return View(maintanance);
        }

        // POST: Maintanances/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Maintanance maintanance = db.maintanance.Find(id);
            db.maintanance.Remove(maintanance);
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
    }
}

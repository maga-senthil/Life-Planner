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
    public class FamiliesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Families
        public ActionResult Index()
        {
            return View(db.family.ToList());
        }

        // GET: Families/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Family family = db.family.Find(id);
            if (family == null)
            {
                return HttpNotFound();
            }
            return View(family);
        }

        // GET: Families/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Families/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Points,Amount")] Family family)
        {
            if (ModelState.IsValid)
            {
                db.family.Add(family);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(family);
        }

        // GET: Families/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Family family = db.family.Find(id);
            if (family == null)
            {
                return HttpNotFound();
            }
            return View(family);
        }

        // POST: Families/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Points,Amount")] Family family)
        {
            if (ModelState.IsValid)
            {
                db.Entry(family).State = EntityState.Modified;
                db.SaveChanges();
                //return RedirectToAction("Index");
                return RedirectToAction("FamilyView");

            }
            return View(family);
        }

        // GET: Families/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Family family = db.family.Find(id);
            if (family == null)
            {
                return HttpNotFound();
            }
            return View(family);
        }

        // POST: Families/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Family family = db.family.Find(id);
            db.family.Remove(family);
            db.SaveChanges();
            // return RedirectToAction("Index");
            return RedirectToAction("FamilyView");

        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public ActionResult FamilyView()
        {
            FamilyViewModel model = new FamilyViewModel();
            model.FamilyList = db.family;
            return View(model);
        }
        [HttpPost]
        public ActionResult FamilyView(FamilyViewModel model)
        {
            if (ModelState.IsValid)
            {
                model.FamilyList = db.family;

                var newFamily = new Family 
                {
                    Name = model.Name,
                    Points = 0,
                    Amount =0
                };
                db.family.Add(newFamily);
                db.SaveChanges();

            }
            return View(model);
        }

    }
}

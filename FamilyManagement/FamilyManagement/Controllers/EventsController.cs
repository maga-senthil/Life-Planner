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
    public class EventsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Events
        public ActionResult Index()
        {
            List<Events> EventList = new List<Events>();
            EventList = db.events.Select(a => a).ToList();
            return View(EventList);
           // return View(db.events.ToList());
        }

        // GET: Events/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Events events = db.events.Find(id);
            if (events == null)
            {
                return HttpNotFound();
            }
            return View(events);
        }

        // GET: Events/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Events/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,title,start,end")] Events events)
        {
            if (ModelState.IsValid)
            {
                db.events.Add(events);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(events);
        }

        // GET: Events/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Events events = db.events.Find(id);
            if (events == null)
            {
                return HttpNotFound();
            }
            return View(events);
        }

        // POST: Events/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,title,start,end")] Events events)
        {
            if (ModelState.IsValid)
            {
                db.Entry(events).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(events);
        }

        // GET: Events/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Events events = db.events.Find(id);
            if (events == null)
            {
                return HttpNotFound();
            }
            return View(events);
        }

        // POST: Events/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Events events = db.events.Find(id);
            db.events.Remove(events);
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
        public ActionResult CalenderEvent()
        {
            List<Maintanance> mainanancelist = new List<Maintanance>();
            mainanancelist = db.maintanance.Select(b => b).ToList();
            List<Events> EventList = new List<Events>();
            EventList = db.events.Select(a => a).ToList();
            return View(EventList);
        }
        public ActionResult Calendar()
        {
            List<Maintanance> mainanancelist = new List<Maintanance>();
            mainanancelist = db.maintanance.Select(b => b).ToList();
            List<Events> EventList = new List<Events>();
            EventList = db.events.Select(a => a).ToList();
            return View(EventList);
        }
    }
}

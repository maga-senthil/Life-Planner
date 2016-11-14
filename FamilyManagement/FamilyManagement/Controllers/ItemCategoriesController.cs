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
    public class ItemCategoriesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ItemCategories
        public ActionResult Index()
        {
            var itemCategory = db.itemCategory.Include(i => i.Categorys);
            return View(itemCategory.ToList());
        }

        // GET: ItemCategories/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ItemCategory itemCategory = db.itemCategory.Find(id);
            if (itemCategory == null)
            {
                return HttpNotFound();
            }
            return View(itemCategory);
        }

        // GET: ItemCategories/Create
        public ActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(db.category, "Id", "CategoryName");
            return View();
        }

        // POST: ItemCategories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,CategoryId,ItemName")] ItemCategory itemCategory)
        {
            if (ModelState.IsValid)
            {
                db.itemCategory.Add(itemCategory);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CategoryId = new SelectList(db.category, "Id", "CategoryName", itemCategory.CategoryId);
            return View(itemCategory);
        }

        // GET: ItemCategories/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ItemCategory itemCategory = db.itemCategory.Find(id);
            if (itemCategory == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryId = new SelectList(db.category, "Id", "CategoryName", itemCategory.CategoryId);
            return View(itemCategory);
        }

        // POST: ItemCategories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,CategoryId,ItemName")] ItemCategory itemCategory)
        {
            if (ModelState.IsValid)
            {
                db.Entry(itemCategory).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("ItemCategoryView");
                // return RedirectToAction("Index");
            }
            ViewBag.CategoryId = new SelectList(db.category, "Id", "CategoryName", itemCategory.CategoryId);
            return View(itemCategory);
        }

        // GET: ItemCategories/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ItemCategory itemCategory = db.itemCategory.Find(id);
            if (itemCategory == null)
            {
                return HttpNotFound();
            }
            return View(itemCategory);
        }

        // POST: ItemCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ItemCategory itemCategory = db.itemCategory.Find(id);
            db.itemCategory.Remove(itemCategory);
            db.SaveChanges();
            return RedirectToAction("ItemCategoryView");

           // return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        public ActionResult ItemCategoryView()
        {
            ItemCategoryViewModel model = new ItemCategoryViewModel();
            model.CategoryList = db.category;
            model.ItemCategoryList = db.itemCategory;
            ViewBag.CategoryId = new SelectList(db.category, "Id", "CategoryName", model.CategoryId);
            return View(model);
        }
        [HttpPost]
        public ActionResult ItemCategoryView(ItemCategoryViewModel model)
        {
            model.CategoryList = db.category;
            model.ItemCategoryList = db.itemCategory;

            var newItemCategory = new ItemCategory
            {
                CategoryId = model.CategoryId,
                ItemName = model.ItemName
            };
            db.itemCategory.Add(newItemCategory);
            db.SaveChanges();
            ViewBag.CategoryId = new SelectList(db.category, "Id", "CategoryName", model.CategoryId);
            return View(model);
        }

    }
}

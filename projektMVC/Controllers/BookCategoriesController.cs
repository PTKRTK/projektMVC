using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using projektMVC.Models;

namespace projektMVC.Controllers
{
    public class BookCategoriesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();


        // GET: BookCategories
        [Authorize(Roles = "Employer, Admin")]
        public ActionResult Index()
        {
            return View(db.BookCategories.ToList());
        }

        // GET: BookCategories/Details/5
        [Authorize(Roles = "Employer, Admin")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BookCategory bookCategory = db.BookCategories.Find(id);
            if (bookCategory == null)
            {
                return HttpNotFound();
            }
            return View(bookCategory);
        }

        // GET: BookCategories/Create
        [Authorize(Roles = "Employer, Admin")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: BookCategories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Employer, Admin")]
        public ActionResult Create([Bind(Include = "BookCategoryID,CategoryTitle")] BookCategory bookCategory)
        {
            if (ModelState.IsValid)
            {
                db.BookCategories.Add(bookCategory);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(bookCategory);
        }

        // GET: BookCategories/Edit/5
        [Authorize(Roles = "Employer, Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BookCategory bookCategory = db.BookCategories.Find(id);
            if (bookCategory == null)
            {
                return HttpNotFound();
            }
            return View(bookCategory);
        }

        // POST: BookCategories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Employer, Admin")]
        public ActionResult Edit([Bind(Include = "BookCategoryID,CategoryTitle")] BookCategory bookCategory)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bookCategory).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(bookCategory);
        }

        // GET: BookCategories/Delete/5
        [Authorize(Roles = "Employer, Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BookCategory bookCategory = db.BookCategories.Find(id);
            if (bookCategory == null)
            {
                return HttpNotFound();
            }
            return View(bookCategory);
        }

        // POST: BookCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Employer, Admin")]
        public ActionResult DeleteConfirmed(int id)
        {
            BookCategory bookCategory = db.BookCategories.Find(id);
            db.BookCategories.Remove(bookCategory);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Employer, Admin")]
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

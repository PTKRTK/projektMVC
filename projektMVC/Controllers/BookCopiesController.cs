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
    public class BookCopiesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: BookCopies
        public ActionResult Index()
        {
            var bookCopies = db.BookCopies.Include(b => b.Book);
            return View(bookCopies.ToList());
        }

        // GET: BookCopies/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BookCopy bookCopy = db.BookCopies.Find(id);
            if (bookCopy == null)
            {
                return HttpNotFound();
            }
            return View(bookCopy);
        }

        // GET: BookCopies/Create
        public ActionResult Create()
        {
            ViewBag.BookID = new SelectList(db.Books, "BookID", "BookID");
            return View();
        }

        // POST: BookCopies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BookCopyID,ISBN,BookCopyReleaseYear,BookID")] BookCopy bookCopy)
        {
            if (ModelState.IsValid)
            {
                db.BookCopies.Add(bookCopy);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.BookID = new SelectList(db.Books, "BookID", "BookID", bookCopy.BookID);
            return View(bookCopy);
        }

        // GET: BookCopies/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BookCopy bookCopy = db.BookCopies.Find(id);
            if (bookCopy == null)
            {
                return HttpNotFound();
            }
            ViewBag.BookID = new SelectList(db.Books, "BookID", "BookID", bookCopy.BookID);
            return View(bookCopy);
        }

        // POST: BookCopies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BookCopyID,ISBN,BookCopyReleaseYear,BookID")] BookCopy bookCopy)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bookCopy).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BookID = new SelectList(db.Books, "BookID", "BookID", bookCopy.BookID);
            return View(bookCopy);
        }

        // GET: BookCopies/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BookCopy bookCopy = db.BookCopies.Find(id);
            if (bookCopy == null)
            {
                return HttpNotFound();
            }
            return View(bookCopy);
        }

        // POST: BookCopies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BookCopy bookCopy = db.BookCopies.Find(id);
            db.BookCopies.Remove(bookCopy);
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

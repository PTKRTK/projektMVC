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
    public class BookAuthorsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: BookAuthors
        [Authorize(Roles = "Employer, Admin")]
        public ActionResult Index()
        {
            var bookAuthors = db.BookAuthors.Include(b => b.Author).Include(b => b.Book);
            return View(bookAuthors.ToList());
        }

        // GET: BookAuthors/Details/5
        [Authorize(Roles = "Employer, Admin")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BookAuthor bookAuthor = db.BookAuthors.Find(id);
            if (bookAuthor == null)
            {
                return HttpNotFound();
            }
            return View(bookAuthor);
        }

        // GET: BookAuthors/Create
        [Authorize(Roles = "Employer, Admin")]
        public ActionResult Create()
        {
            ViewBag.BookID= new SelectList(db.Books, "BookID", "BookTitle");
            ViewBag.AuthorID = new SelectList(db.Authors, "AuthorID", "Name");
            return View();
        }

        // POST: BookAuthors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Employer, Admin")]
        public ActionResult Create([Bind(Include = "BookAuthorID,BookID,AuthorID")] BookAuthor bookAuthor)
        {
            if (ModelState.IsValid)
            {
                db.BookAuthors.Add(bookAuthor);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AuthorID = new SelectList(db.Authors, "AuthorID", "Name", bookAuthor.AuthorID);
            ViewBag.BookID = new SelectList(db.Books, "BookID", "BookID", bookAuthor.BookID);
            return View(bookAuthor);
        }

        // GET: BookAuthors/Edit/5
        [Authorize(Roles = "Employer, Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BookAuthor bookAuthor = db.BookAuthors.Find(id);
            if (bookAuthor == null)
            {
                return HttpNotFound();
            }
            ViewBag.AuthorID = new SelectList(db.Authors, "AuthorID", "Name", bookAuthor.AuthorID);
            ViewBag.BookID = new SelectList(db.Books, "BookID", "BookID", bookAuthor.BookID);
            return View(bookAuthor);
        }

        // POST: BookAuthors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Employer, Admin")]
        public ActionResult Edit([Bind(Include = "BookAuthorID,BookID,AuthorID")] BookAuthor bookAuthor)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bookAuthor).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AuthorID = new SelectList(db.Authors, "AuthorID", "Name", bookAuthor.AuthorID);
            ViewBag.BookID = new SelectList(db.Books, "BookID", "BookID", bookAuthor.BookID);
            return View(bookAuthor);
        }

        // GET: BookAuthors/Delete/5
        [Authorize(Roles = "Employer, Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BookAuthor bookAuthor = db.BookAuthors.Find(id);
            if (bookAuthor == null)
            {
                return HttpNotFound();
            }
            return View(bookAuthor);
        }

        // POST: BookAuthors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Employer, Admin")]
        public ActionResult DeleteConfirmed(int id)
        {
            BookAuthor bookAuthor = db.BookAuthors.Find(id);
            db.BookAuthors.Remove(bookAuthor);
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

using projektMVC.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace projektMVC.Controllers
{
    public class BorrowController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Borrows
        public ActionResult Index()
        {
            var borrows = db.Borrows.Include(b => b.BookCopy).Include(b => b.Punishment).Include(b => b.User);
            return View(borrows.ToList());
        }

        // GET: Borrows/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Borrow borrow = db.Borrows.Find(id);
            if (borrow == null)
            {
                return HttpNotFound();
            }
            return View(borrow);
        }

        // GET: Borrows/Create
        public ActionResult Create()
        {
            ViewBag.BookCopyID = new SelectList(db.BookCopies, "BookCopyID", "BookCopyID");
            ViewBag.PunishmentID = new SelectList(db.Punishments, "PunishmentID", "PunishmentID");
            ViewBag.UserID = new SelectList(db.ApplicationUsers, "UserID", "Name");
            return View();
        }

        // POST: Borrows/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BorrowID,BorrowDate,ReturnDate,BookCopyID,UserID,PunishmentID")] Borrow borrow)
        {
            if (ModelState.IsValid)
            {
                db.Borrows.Add(borrow);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.BookCopyID = new SelectList(db.BookCopies, "BookCopyID", "BookCopyID", borrow.BookCopyID);
            ViewBag.PunishmentID = new SelectList(db.Punishments, "PunishmentID", "PunishmentID", borrow.PunishmentID);
            ViewBag.UserID = new SelectList(db.ApplicationUsers, "Id", "Name", borrow.UserID);
            return View(borrow);
        }

        // GET: Borrows/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Borrow borrow = db.Borrows.Find(id);
            if (borrow == null)
            {
                return HttpNotFound();
            }
            ViewBag.BookCopyID = new SelectList(db.BookCopies, "BookCopyID", "BookCopyID", borrow.BookCopyID);
            ViewBag.PunishmentID = new SelectList(db.Punishments, "PunishmentID", "PunishmentID", borrow.PunishmentID);
            ViewBag.UserID = new SelectList(db.ApplicationUsers, "Id", "Name", borrow.UserID);
            return View(borrow);
        }

        // POST: Borrows/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BorrowID,BorrowDate,ReturnDate,BookCopyID,UserID,PunishmentID")] Borrow borrow)
        {
            if (ModelState.IsValid)
            {
                db.Entry(borrow).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BookCopyID = new SelectList(db.BookCopies, "BookCopyID", "BookCopyID", borrow.BookCopyID);
            ViewBag.PunishmentID = new SelectList(db.Punishments, "PunishmentID", "PunishmentID", borrow.PunishmentID);
            ViewBag.UserID = new SelectList(db.ApplicationUsers, "Id", "Name", borrow.UserID);
            return View(borrow);
        }

        // GET: Borrows/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Borrow borrow = db.Borrows.Find(id);
            if (borrow == null)
            {
                return HttpNotFound();
            }
            return View(borrow);
        }

        // POST: Borrows/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Borrow borrow = db.Borrows.Find(id);
            borrow.BookCopy.IsBorrowed = "Free";
            db.Entry(borrow).State = EntityState.Modified;
            db.Borrows.Remove(borrow);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        public ActionResult BorrowForUser(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Borrow borrow = db.Borrows.Find(id);
            if (borrow == null)
            {
                return HttpNotFound();
            }
            if (borrow.BookCopy.IsBorrowed.Equals("Reserved")) 
            {
                borrow.BookCopy.IsBorrowed = "Borrowed";
                db.Entry(borrow).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View();
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
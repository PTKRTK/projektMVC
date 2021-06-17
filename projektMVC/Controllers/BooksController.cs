using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Timers;
using System.Web;
using System.Web.Mvc;
using projektMVC.Models;

namespace projektMVC.Controllers
{
    public class BooksController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Books
        [Authorize(Roles = "Employer, Admin")]
        public ActionResult Index()
        {
            var books = db.Books.Include(b => b.BookCategory).Include(b => b.PublishingHouse);
            return View(books.ToList());
        }

        [Authorize(Roles = "Employer, Admin")]
        public void NotificationForReturnBook()
        {
            var users = db.Users.ToList();
            var borrows = db.Borrows.ToList();
            string email = "";

            foreach(Borrow b in borrows)
            {
                if (b.BorrowDate.AddDays(7).Equals(DateTime.Now))
                {
                    foreach(ApplicationUser u in users)
                    {
                        if(u.Id == b.UserID)
                            email = u.Email;
                    }
                }
            }

            try
            {
                if (ModelState.IsValid)
                {
                    var senderEmail = new MailAddress("javaprojekt2137@gmail.com", "ADMINISTRATOR BIBLIOTEKI");
                    var receiverEmail = new MailAddress(email, "Receiver");
                    var password = "cxwmgeplhwgqnmot";
                    var sub = "Powiadomienie o zwrocie";
                    var body = "ODDAJ TĄ KSIĄŻKĘ WRESZCIE";
                    var smtp = new SmtpClient
                    {
                        Host = "smtp.gmail.com",
                        Port = 587,
                        EnableSsl = true,
                        DeliveryMethod = SmtpDeliveryMethod.Network,
                        UseDefaultCredentials = false,
                        Credentials = new NetworkCredential(senderEmail.Address, password)
                    };
                    using (var mess = new MailMessage(senderEmail, receiverEmail)
                    {
                        Subject = sub,
                        Body = body
                    })
                    {
                        smtp.Send(mess);
                    }
                }
            }
            catch (Exception)
            {
                ViewBag.Error = "Some Error";
            }
        }




        GET: Books/Details/5
        [Authorize(Roles = "Employer")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = db.Books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        // GET: Books/Create
        [Authorize(Roles = "Employer")]
        public ActionResult Create()
        {
            ViewBag.BookCategoryID = new SelectList(db.BookCategories, "BookCategoryID", "CategoryTitle");
            ViewBag.PublishingHouseID = new SelectList(db.PublishingHouses, "PublishingHouseID", "Name");
            return View();
        }

        // POST: Books/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Employer")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BookID,BookTitle,PublicationYear,PublishingHouseID,BookCategoryID")] Book book)
        {
            if (ModelState.IsValid)
            {
                db.Books.Add(book);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.BookCategoryID = new SelectList(db.BookCategories, "BookCategoryID", "CategoryTitle", book.BookCategoryID);
            ViewBag.PublishingHouseID = new SelectList(db.PublishingHouses, "PublishingHouseID", "Name", book.PublishingHouseID);
            return View(book);
        }

        // GET: Books/Edit/5
        [Authorize(Roles = "Employer")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = db.Books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            ViewBag.BookCategoryID = new SelectList(db.BookCategories, "BookCategoryID", "CategoryTitle", book.BookCategoryID);
            ViewBag.PublishingHouseID = new SelectList(db.PublishingHouses, "PublishingHouseID", "Name", book.PublishingHouseID);
            return View(book);
        }

        // POST: Books/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Employer")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BookID,BookTitle,PublicationYear,PublishingHouseID,BookCategoryID")] Book book)
        {
            if (ModelState.IsValid)
            {
                db.Entry(book).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BookCategoryID = new SelectList(db.BookCategories, "BookCategoryID", "CategoryTitle", book.BookCategoryID);
            ViewBag.PublishingHouseID = new SelectList(db.PublishingHouses, "PublishingHouseID", "Name", book.PublishingHouseID);
            return View(book);
        }

        // GET: Books/Delete/5
        [Authorize(Roles = "Employer")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = db.Books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        // POST: Books/Delete/5
        [Authorize(Roles = "Employer")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Book book = db.Books.Find(id);
            db.Books.Remove(book);
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

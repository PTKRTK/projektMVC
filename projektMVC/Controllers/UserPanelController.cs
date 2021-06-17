using projektMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.Data;
using System.Net;
using Microsoft.AspNet.Identity;
using System.Net.Mail;

namespace projektMVC.Controllers
{
    public class UserPanelController : Controller
    {
   
		private ApplicationDbContext db = new ApplicationDbContext();
        private ApplicationDbContext db2 = new ApplicationDbContext();


        [Authorize(Roles = "User")]
        public ActionResult Index()
		{
            //dodane
              foreach (Borrow b in db2.Borrows)
            {
                    if ((b.ReturnDate - DateTime.Today).TotalDays < 0)
                    {
                 
                        b.PunishmentID = 2;
                        db2.Entry(b).State = EntityState.Modified;
                
                    }
                    if ((b.ReturnDate - DateTime.Today).TotalDays < -10)
                    {
                        
                        b.PunishmentID = 3;
                        db2.Entry(b).State = EntityState.Modified;
       
                    }
                    if ((b.ReturnDate - DateTime.Today).TotalDays < -20)
                    {
                        b.PunishmentID = 4;
                        db2.Entry(b).State = EntityState.Modified;
                    
                    }

                }

            db2.SaveChanges();
            //stare
            var userId = User.Identity.GetUserId();
			var books = db.Borrows.Where(b => b.UserID == userId).Include(b => b.BookCopy.Book).Include(b => b.BookCopy).Include(b => b.Punishment);

			return View(books.ToList());
		}
        [Authorize(Roles = "User")]
        public JsonResult GetAuthors()
		{
			return Json(db.Authors.ToList());
		}
        [Authorize(Roles = "User")]
        public JsonResult GetBooks()
		{
			var books = db.Books.Include(b => b.BookCategory).Include(b => b.PublishingHouse);
			return Json(books.ToList());
		}

        [Authorize(Roles = "User")]
        public ActionResult BorrowBook()
        {
            var userId = User.Identity.GetUserId();
            var getCopies = db.BookCopies.Where(b=>b.IsBorrowed.Equals("Free")).Include(b=> b.Book);


            ViewBag.BookCopyID = new SelectList(db.BookCopies.Where(b => b.IsBorrowed.Equals("Free")), "BookCopyID", "ISBN");

     

            ViewBag.UserID = userId;
            return View();
        }

    
        // POST: Borrows/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "User")]
        public ActionResult BorrowBook([Bind(Include = "BorrowID,BorrowDate,ReturnDate,BookCopyID,UserID,PunishmentID")] Borrow borrow)
        {

            if (ModelState.IsValid)
            {
                borrow.UserID = User.Identity.GetUserId();
                borrow.BorrowDate = DateTime.Today;
                borrow.ReturnDate = DateTime.Today.AddMonths(1);
                borrow.PunishmentID = 1;

                //zmienione
                var bookCopyUpdate = db.BookCopies.Find(borrow.BookCopyID);
                bookCopyUpdate.IsBorrowed = "Reserved";
                db.Entry(bookCopyUpdate).State = EntityState.Modified;

                NotificationForBookIsToGet(User.Identity.GetUserId());
                db.Borrows.Add(borrow);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.BookCopyID = new SelectList(db.BookCopies, "BookCopyID", "BookCopyID", borrow.BookCopyID);
            ViewBag.PunishmentID = new SelectList(db.Punishments, "PunishmentID", "PunishmentID", borrow.PunishmentID);
            ViewBag.UserID = new SelectList(db.ApplicationUsers, "Id", "Name", borrow.UserID);
            return View(borrow);
        }



        [Authorize(Roles = "User")]
        protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				db.Dispose();
			}
			base.Dispose(disposing);
		}




        public JsonResult GetBookCopies()
        {
            var books = db.BookCopies;
            return Json(books.ToList());
        }

        public void NotificationForBookIsToGet(string userId)
        {
            var users = db.Users.ToList();
            string email = "";

            foreach (ApplicationUser u in users)
            {
                if (u.Id == userId)
                    email = u.Email;
            }
            try
            {
                if (ModelState.IsValid)
                {
                    var senderEmail = new MailAddress("javaprojekt2137@gmail.com", "ADMINISTRATOR BIBLIOTEKI");
                    var receiverEmail = new MailAddress(email, "Receiver");
                    var password = "cxwmgeplhwgqnmot";
                    var sub = "Powiadomienie";
                    var body = "TWOJA KSIAZKA CZEKA NA ODBIÓR";
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




        /*

        [Authorize(Roles = "User")]
     
        public ActionResult Search(string search)
        {
            var book_ = db.Books.Include(b => b.BookCategory);


            if (!String.IsNullOrEmpty(search))
            {
                book_ = book_.Where(s => s.BookTitle.Contains(search) || s.BookCategory.CategoryTitle.Contains(search));


            }

            return View(book_.ToList());

        } */



        [Authorize(Roles = "User")]

        public ActionResult Search(string search)
        {
            var bookCopies_ = db.BookCopies.Include(b => b.Book).Include(b => b.Book.BookCategory);


            if (!String.IsNullOrEmpty(search))
            {
                bookCopies_ = bookCopies_.Where(s => s.Book.BookTitle.Contains(search) || s.Book.BookCategory.CategoryTitle.Contains(search) || s.ISBN.ToString().Contains(search));
                


            }

            return View(bookCopies_.ToList());

        }



        [Authorize(Roles = "User")]
        public ActionResult BorrowBySearch(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var bookCopies = db.BookCopies.Where(b => b.BookID.Equals(id) && b.IsBorrowed.Equals("Free"));
     
            if (bookCopies == null)
            {
                return View();
            }
            ViewBag.BookCopyID = new SelectList(db.BookCopies, "BookCopyID", "IdKopii", bookCopies.ElementAt(0).BookCopyID);
            return View(bookCopies.ElementAt(0).BookCopyID);
        }

    }   
}
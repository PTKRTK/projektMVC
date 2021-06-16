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

namespace projektMVC.Controllers
{
    public class UserPanelController : Controller
    {
   
		private ApplicationDbContext db = new ApplicationDbContext();

		public ActionResult Index()
		{
			ViewBag.AuthorsList = new LinkedList<SelectListItem>();
			ViewBag.BooksList = new LinkedList<SelectListItem>();

			return View();
		}

		public ActionResult Borrowed()
		{
			var userId = User.Identity.GetUserId();
			var books = db.Borrows.Where(b => b.UserID == userId).Include(b => b.BookCopy.Book).Include(b => b.BookCopy);

			return View(books.ToList());
		}

		public JsonResult GetAuthors()
		{
			return Json(db.Authors.ToList());
		}

		public JsonResult GetBooks()
		{
			var books = db.Books.Include(b => b.BookCategory).Include(b => b.PublishingHouse);
			return Json(books.ToList());
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
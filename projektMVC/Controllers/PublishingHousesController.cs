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
    public class PublishingHousesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: PublishingHouses
        public ActionResult Index()
        {
            return View(db.PublishingHouses.ToList());
        }

        // GET: PublishingHouses/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PublishingHouse publishingHouse = db.PublishingHouses.Find(id);
            if (publishingHouse == null)
            {
                return HttpNotFound();
            }
            return View(publishingHouse);
        }

        // GET: PublishingHouses/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PublishingHouses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PublishingHouseID,Name,City")] PublishingHouse publishingHouse)
        {
            if (ModelState.IsValid)
            {
                db.PublishingHouses.Add(publishingHouse);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(publishingHouse);
        }

        // GET: PublishingHouses/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PublishingHouse publishingHouse = db.PublishingHouses.Find(id);
            if (publishingHouse == null)
            {
                return HttpNotFound();
            }
            return View(publishingHouse);
        }

        // POST: PublishingHouses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PublishingHouseID,Name,City")] PublishingHouse publishingHouse)
        {
            if (ModelState.IsValid)
            {
                db.Entry(publishingHouse).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(publishingHouse);
        }

        // GET: PublishingHouses/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PublishingHouse publishingHouse = db.PublishingHouses.Find(id);
            if (publishingHouse == null)
            {
                return HttpNotFound();
            }
            return View(publishingHouse);
        }

        // POST: PublishingHouses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PublishingHouse publishingHouse = db.PublishingHouses.Find(id);
            db.PublishingHouses.Remove(publishingHouse);
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

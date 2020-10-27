using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using OnlineFlightBooking.Models;

namespace OnlineFlightBooking.Controllers
{
    public class PlainsController : Controller
    {
        private MyDB db = new MyDB();

        // GET: Plains
        public ActionResult Index()
        {
            return View(db.Plains.ToList());
        }

        // GET: Plains/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Plain plain = db.Plains.Find(id);
            if (plain == null)
            {
                return HttpNotFound();
            }
            return View(plain);
        }

        // GET: Plains/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Plains/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PlainID,PlainTotalSeats,PlainNumber")] Plain plain)
        {
            if (ModelState.IsValid)
            {
                db.Plains.Add(plain);
                db.SaveChanges();

                var query =
                    from p in db.Plains
                    where p.PlainID.Equals(plain.PlainID)
                    select p;

                foreach(Plain p in query)
                {
                    p.PlainNumber = p.PlainID;
                }

                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(plain);
        }

        // GET: Plains/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Plain plain = db.Plains.Find(id);
            if (plain == null)
            {
                return HttpNotFound();
            }
            return View(plain);
        }

        // POST: Plains/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PlainID,PlainTotalSeats")] Plain plain)
        {
            if (ModelState.IsValid)
            {
                db.Entry(plain).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(plain);
        }

        // GET: Plains/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Plain plain = db.Plains.Find(id);
            if (plain == null)
            {
                return HttpNotFound();
            }
            return View(plain);
        }

        // POST: Plains/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Plain plain = db.Plains.Find(id);
            db.Plains.Remove(plain);
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

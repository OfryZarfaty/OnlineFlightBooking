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
    public class CreditCardsController : Controller
    {
        private MyDB db = new MyDB();

        // GET: CreditCards
        public ActionResult Index()
        {
            return View(db.CreditCards.ToList());
        }

        // GET: CreditCards/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CreditCard creditCard = db.CreditCards.Find(id);
            if (creditCard == null)
            {
                return HttpNotFound();
            }
            return View(creditCard);
        }

        // GET: CreditCards/Create
        public ActionResult Create()
        {
            Person p = db.People.Find(Session["UserId"]);
            if (p.CreditCardID != null)
            {
                CreditCard c = db.CreditCards.Find(p.CreditCardID);
                return View(c);
            }
            ViewBag.personID = p.PersonID;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CreditCardID,FirstName,LastName,CardNumber,DateExpired,CVV")] CreditCard creditCard)
        {
            if (creditCard.CreditCardID ==0)
            {
                creditCard.Person = db.People.Find(Session["UserId"]);
                db.CreditCards.Add(creditCard);
                db.SaveChanges();

                Person p = db.People.Find(Session["UserId"]);
                p.CreditCardID = creditCard.CreditCardID;
                db.SaveChanges();
            }
            else
            {
                if (ModelState.IsValid)
                {
                    db.Entry(creditCard).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }
            ViewBag.CreditCardID = new SelectList(db.CreditCards, "CreditCardID", "CreditCardID", creditCard.CreditCardID);
            return RedirectToAction("Home", "People");
        }

        // GET
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CreditCard creditCard = db.CreditCards.Find(id);
            if (creditCard == null)
            {
                return HttpNotFound();
            }
            return View(creditCard);
        }

        // POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CreditCardID,FirstName,LastName,CardNumber,DateExpired,CVV")] CreditCard creditCard)
        {
            if (ModelState.IsValid)
            {
                db.Entry(creditCard).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(creditCard);
        }

        // GET: CreditCards/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CreditCard creditCard = db.CreditCards.Find(id);
            if (creditCard == null)
            {
                return HttpNotFound();
            }
            return View(creditCard);
        }

        // POST: CreditCards/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CreditCard creditCard = db.CreditCards.Find(id);
            db.CreditCards.Remove(creditCard);
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

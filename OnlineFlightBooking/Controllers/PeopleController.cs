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
    public class PeopleController : Controller
    {
        private MyDB db = new MyDB();

        public ActionResult Home()
        {
            return View();
        }

        // GET: People
        public ActionResult Index()
        {
            return View(db.People.ToList());
        }

        // GET: People/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Person person = db.People.Find(id);
            if (person == null)
            {
                return HttpNotFound();
            }
            return View(person);
        }

        // GET
        public ActionResult Create()
        {
            return View();
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PersonID,FirstName,LastName,UserName,Password,Permission,CreditCardID")] Person person)
        {
            if (ModelState.IsValid)
            {
                person.Permission = 0;
                db.People.Add(person);
                db.SaveChanges();
                return RedirectToAction("Login");
            }
            else
            {
                ModelState.AddModelError("", "Some Error Occured!");
            }
            return View(person);
        }

        // GET: People/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Person person = db.People.Find(id);
            if (person == null)
            {
                return HttpNotFound();
            }
            return View(person);
        }

        // POST: People/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PersonID,FirstName,LastName,UserName,Password,Permission,CreditCardID")] Person person)
        {
            if (ModelState.IsValid)
            {
                db.Entry(person).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(person);
        }

        // GET: People/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Person person = db.People.Find(id);
            if (person == null)
            {
                return HttpNotFound();
            }
            return View(person);
        }

        // POST: People/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Person person = db.People.Find(id);
            db.People.Remove(person);
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

        public ActionResult LogIn()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogIn(Person person)
        {
            if (ModelState.IsValid)
            {
                using (MyDB db = new MyDB())
                {
                    if (person.Permission == 1)
                    {
                        Admin admin = new Admin();
                        admin = db.Admins.Where(a => a.UserName.Equals(person.UserName) && a.Password.Equals(person.Password)).FirstOrDefault();
                        if (admin != null)
                        {
                            Session["UserId"] = admin.AdminID;
                            //Session["UserName"] = admin.UserName.ToString();
                            //Session["type"] = "Admin";
                            return RedirectToAction("Index", "Admin");
                        }
                    }
                    else //person.Permission == 0
                    {
                        Person customer = new Person();
                        customer = db.People.Where(p => p.UserName.Equals(person.UserName) && p.Password.Equals(person.Password)).FirstOrDefault();
                        if (customer != null)
                        {
                            Session["UserId"] = customer.PersonID;
                            //Session["UserName"] = customer.UserName.ToString();
                            //Session["type"] = "Person";
                            return RedirectToAction("SearchOneWay", "Flights");
                        }
                    }
                    ViewData["notLegedIn"] = "Username or passward was incorrect";
                }
            }
            return View(person);
        }
    }
}

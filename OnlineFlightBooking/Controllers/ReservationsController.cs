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
    public class ReservationsController : Controller
    {
        private MyDB db = new MyDB();

        // GET: Reservations
        public ActionResult Index()
        {
            var reservations = db.Reservations.Include(r => r.Flight).Include(r => r.Person);
            return View(reservations.ToList());
        }

        // GET: Reservations/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reservation reservation = db.Reservations.Find(id);
            if (reservation == null)
            {
                return HttpNotFound();
            }
            Flight flight = db.Flights.Find(reservation.FlightID);
            ViewBag.FlightNumber = flight.FlightNumber;
            return View(reservation);
        }

        //GET
        public ActionResult Create(int? flightID)
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(int flightID, int numOfPassangers)
        {
            if (Session["UserId"] != null)
            {
                Person person = db.People.Find(Session["UserId"]); 
                Reservation reservation = new Reservation();
                reservation.PersonID = person.PersonID;
                reservation.Person = db.People.Find(person.PersonID);
                reservation.FlightID = flightID;
                reservation.Flight = db.Flights.Find(flightID);
                reservation.FinalPrice=(reservation.Flight.FlightPrice)*numOfPassangers;
                if (ModelState.IsValid)
                {
                    db.Reservations.Add(reservation);
                    person.Reservations.Add(reservation);
                    db.SaveChanges();
                    Update(reservation.PersonID, reservation.FlightID, reservation.ReservationID, numOfPassangers);
                }
                ViewBag.person = person;
                ViewBag.numOfPassangers = numOfPassangers;
                ViewBag.price = reservation.FinalPrice;
                return View(person.Reservations.ToList());
                //return RedirectToAction();
            }
            return RedirectToAction("Index");
        }
        public void Update(int personID, int flightID, int reservationID, int numOfPassangers)
        {
            Person person = db.People.Find(personID);
            Reservation reservation = db.Reservations.Find(reservationID);
            Flight flight = db.Flights.Find(flightID);
            person.Reservations.Add(reservation);
            flight.FlightTotalAviableSeats = flight.FlightTotalAviableSeats - numOfPassangers;
            db.SaveChanges();
        }
        // GET: Reservations/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reservation reservation = db.Reservations.Find(id);
            if (reservation == null)
            {
                return HttpNotFound();
            }
            ViewBag.FlightID = new SelectList(db.Flights, "FlightID", "FlightNumber", reservation.FlightID);
            ViewBag.PersonID = new SelectList(db.People, "PersonID", "FirstName", reservation.PersonID);
            return View(reservation);
        }

        // POST: Reservations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ReservationID,PersonID,FlightID,FinalPrice")] Reservation reservation)
        {
            if (ModelState.IsValid)
            {
                db.Entry(reservation).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.FlightID = new SelectList(db.Flights, "FlightID", "FlightNumber", reservation.FlightID);
            ViewBag.PersonID = new SelectList(db.People, "PersonID", "FirstName", reservation.PersonID);
            return View(reservation);
        }

        // GET: Reservations/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reservation reservation = db.Reservations.Find(id);
            if (reservation == null)
            {
                return HttpNotFound();
            }
            return View(reservation);
        }

        // POST: Reservations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Reservation reservation = db.Reservations.Find(id);
            db.Reservations.Remove(reservation);
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

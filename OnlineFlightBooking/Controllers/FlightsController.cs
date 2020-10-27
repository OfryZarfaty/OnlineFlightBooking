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
    public class FlightsController : Controller
    {
        private MyDB db = new MyDB();

        public ActionResult Index()
        {
            var flights = db.Flights.Include(f => f.Plain);
            return View(flights.ToList());
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Flight flight = db.Flights.Find(id);
            if (flight == null)
            {
                return HttpNotFound();
            }
            return View(flight);
        }

        // GET
        public ActionResult Create()
        {
            ViewBag.PlainID = new SelectList(db.Plains, "PlainID", "PlainID");
            return View();
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "FlightID,FlightNumber,FlightFromCountry,FlightToCountry,FlightPrice,FlightDateTimeTakeOff,FlightDuration,FlightDateTimeLanding,PlainID,FlightTotalAviableSeats,FlightStatus")] Flight flight)
        {
            Plain plain = db.Plains.Find(flight.PlainID);
            flight.FlightTotalAviableSeats = plain.PlainTotalSeats;
            flight.FlightDateTimeLanding = flight.FlightDateTimeTakeOff;
            flight.FlightDateTimeLanding = flight.FlightDateTimeLanding.AddHours(flight.FlightDuration);
            flight.FlightStatus = "ON TIME";

            var plains =
                from p in db.Plains
                select p.PlainNumber;
            plains = plains.Distinct();
            ViewBag.allPlains = plains.ToList();

            if (ModelState.IsValid)
            {
                db.Flights.Add(flight);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.PlainID = new SelectList(db.Plains, "PlainID", "PlainID", flight.PlainID);
            return View(flight);
        }

        // GET
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Flight flight = db.Flights.Find(id);
            if (flight == null)
            {
                return HttpNotFound();
            }
            ViewBag.PlainID = new SelectList(db.Plains, "PlainID", "PlainID", flight.PlainID);
            return View(flight);
        }

        // POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "FlightID,FlightNumber,FlightFromCountry,FlightToCountry,FlightPrice,FlightDateTimeTakeOff,FlightDuration,FlightDateTimeLanding,PlainID,FlightTotalAviableSeats,FlightStatus")] Flight flight)
        {
            Plain plain = db.Plains.Find(flight.PlainID);
            flight.FlightTotalAviableSeats = plain.PlainTotalSeats;
            flight.FlightDateTimeLanding = flight.FlightDateTimeTakeOff;
            flight.FlightDateTimeLanding = flight.FlightDateTimeLanding.AddHours(flight.FlightDuration);

            if (ModelState.IsValid)
            {
                db.Entry(flight).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PlainID = new SelectList(db.Plains, "PlainID", "PlainID", flight.PlainID);
            return View(flight);
        }

        // GET: Flights/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Flight flight = db.Flights.Find(id);
            if (flight == null)
            {
                return HttpNotFound();
            }
            return View(flight);
        }

        // POST: Flights/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Flight flight = db.Flights.Find(id);
            db.Flights.Remove(flight);
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

        public List<Flight> ASearch(string flightFromCountry, string flightToCountry, DateTime? flightDateTimeTakeOff, int? numOfPassengers)
        {
            var flights =
                from f in db.Flights
                select f;

            if (!String.IsNullOrEmpty(flightFromCountry))
            {
                flights = flights.Where(f => (f.FlightFromCountry.Equals(flightFromCountry)) && (DateTime.Compare(f.FlightDateTimeTakeOff, DateTime.Today) >= 0));
            }
            if (!String.IsNullOrEmpty(flightToCountry))
            {
                flights = flights.Where(f => (f.FlightToCountry.Equals(flightToCountry)) && (DateTime.Compare(f.FlightDateTimeTakeOff, DateTime.Today) >= 0));
            }
            if (flightDateTimeTakeOff != null)
            {
                flights = flights.Where(f => DateTime.Compare(f.FlightDateTimeTakeOff, flightDateTimeTakeOff.Value) >= 0);
            }
            if (numOfPassengers != null)
            {
                flights = flights.Where(f => f.FlightTotalAviableSeats >= numOfPassengers);
            }
            return flights.ToList();
        }

        public ActionResult SearchOneWay(string flightFromCountry, string flightToCountry, DateTime? flightDateTimeTakeOff, int? numOfPassengers)
        {
            var fromCountryValues =
                from f in db.Flights
                select f.FlightFromCountry;
            fromCountryValues = fromCountryValues.Distinct();
            ViewBag.allfroms = fromCountryValues.ToList();

            var toCountryValues =
                from f in db.Flights
                select f.FlightToCountry;
            toCountryValues = toCountryValues.Distinct();
            ViewBag.alldestinations = toCountryValues.ToList();

            List<Flight> flights = new List<Flight>();
            flights = ASearch(flightFromCountry, flightToCountry, flightDateTimeTakeOff, numOfPassengers);
            if (numOfPassengers != null)
            {
                ViewBag.numOfPassengers = numOfPassengers;
            }
            else
            {
                ViewBag.numOfPassengers = 0;
            }
            return View(flights.ToList());
        }

        public ActionResult SearchRoundWay(string flightFromCountry, string flightToCountry, DateTime? flightDateTimeTakeOff, int? numOfPassengers)
        {
            var fromCountryValues =
                from f in db.Flights
                select f.FlightFromCountry;
            fromCountryValues = fromCountryValues.Distinct();
            ViewBag.allfroms = fromCountryValues.ToList();

            var toCountryValues =
                from f in db.Flights
                select f.FlightToCountry;
            toCountryValues = toCountryValues.Distinct();
            ViewBag.alldestinations = toCountryValues.ToList();

            List<Flight> flightsTo = new List<Flight>();
            flightsTo = ASearch(flightFromCountry, flightToCountry, flightDateTimeTakeOff, numOfPassengers);
            ViewData["flightOut"] = flightsTo.ToList();
            List<Flight> flightsfrom = new List<Flight>();
            flightsfrom = ASearch(flightToCountry, flightFromCountry, flightDateTimeTakeOff, numOfPassengers);
            ViewData["flightIn"] = flightsfrom.ToList();
            if (numOfPassengers != null)
            {
                ViewBag.numOfPassengers = numOfPassengers;
            }
            else
            {
                ViewBag.numOfPassengers = 0;
            }
            return View();
        }

    }
}

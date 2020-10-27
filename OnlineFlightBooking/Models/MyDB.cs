using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace OnlineFlightBooking.Models
{
    public class MyDB : DbContext
    {
        public DbSet<Person> People { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<CreditCard> CreditCards { get; set; }
        public DbSet<Flight> Flights { get; set; }
        public DbSet<Plain> Plains { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
    }
}
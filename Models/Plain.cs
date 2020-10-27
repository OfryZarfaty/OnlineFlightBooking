using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OnlineFlightBooking.Models
{
    public class Plain
    {
        public int PlainID { get; set; } //mispar zanav

        [Display(Name = "Plain Serial Number")]
        public int PlainNumber { get; set; }

        [Display(Name = "Plain Total Seats")]
        public int PlainTotalSeats { get; set; }
        public ICollection<Flight> AllFlightsEverOfPlain { get; set; }
    }
}
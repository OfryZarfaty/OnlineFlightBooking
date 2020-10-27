using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OnlineFlightBooking.Models
{
    public class Reservation
    {
        public int ReservationID { get; set; }
        public int PersonID { get; set; }
        public Person Person { get; set; }
        public int FlightID { get; set; }
        public Flight Flight{get;set;}

        [DataType(DataType.Currency)]
        [Display(Name = "Total Price Of Reservation")]
        public double FinalPrice { get; set; } //according to function in controller
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OnlineFlightBooking.Models
{
    public class Flight
    {
        public int FlightID { get; set; }

        [Display(Name = "Flight Number")]
        public string FlightNumber { get; set; }

        [Display(Name = "From")]
        public string FlightFromCountry { get; set; }

        [Display(Name = "To")]
        public string FlightToCountry { get; set; }

        [DataType(DataType.Currency)]
        [Display(Name = "Price")]
        public double FlightPrice { get; set; }

        [Display(Name = "Departure")]
        public DateTime FlightDateTimeTakeOff { get; set; }

        [Display(Name = "Flight Duration")]
        public int FlightDuration { get; set; }

        [Display(Name = "Arrival")]
        public DateTime FlightDateTimeLanding { get; set; }

        [Display(Name = "Plain Serial Number")]
        public int PlainID { get; set; }
        public Plain Plain { get; set; }

        [Display(Name = "Plain Total Available Seats")]
        public int FlightTotalAviableSeats { get; set; }

        [Display(Name = "Flight Status")]
        public string FlightStatus { get; set; } //on time - 0, delay - 1, cancled - 2
        public ICollection<Reservation> AllReservationsOfSpecificFlight { get; set; }
    }
}
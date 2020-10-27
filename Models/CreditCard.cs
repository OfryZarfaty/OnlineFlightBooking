using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OnlineFlightBooking.Models
{
    public class CreditCard
    {
        public int CreditCardID { get; set; }

        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Display(Name = "Card Number")]
        public int CardNumber { get; set; }

        [Display(Name = "Expiration Date")]
        public DateTime DateExpired { get; set; }
        public int CVV { get; set; }
        public virtual Person Person { get; set; }
    }
}
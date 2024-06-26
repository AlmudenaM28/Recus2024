using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace _2023_VIAJES_LA_TORRIJA.Models
{
    public class BilleteTren
    {
        [Key]
        public string TrainTicketId { get; set; }

        public string Company { get; set; }

        [Display(Name = "Tran type")]
        public string TrainType { get; set; }

        public string Destiny { get; set; }

        public string Origin { get; set; }

        [Display(Name = "Cafeteria")]
        public Boolean HasCafeteria { get; set; }

        [Display(Name = "Number of seats")]
        public int NumberSeats { get; set; }

        public string Date { get; set; }

        [Display(Name = "Departure time")]
        public string DepartureTime { get; set; }

        [Display(Name = "Arrival time")]
        public string ArrivalTime { get; set; }

        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }
    }
}
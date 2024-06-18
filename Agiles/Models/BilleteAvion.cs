using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace _2023_VIAJES_LA_TORRIJA.Models
{
    public class BilleteAvion
    {
        [Key] 
        public int BilleteAvionId { get; set; }

        public string Airline { get; set; }

        [Display(Name = "Origin airport")]
        public string OriginAirport { get; set; }

        [Display(Name = "Destination airport")]
        public string DestinationAirport { get; set; }

        public Boolean LowCost { get; set; }

        [Display(Name = "Number of seats")]
        public int NumberOfPlaces { get; set; }

        public string Date { get; set; }

        [Display(Name = "Departure time")]
        public string DepartureTime { get; set; }

        [Display(Name = "Arrival time")]
        public string ArrivalTime { get; set; }

    }
}
using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _2023_VIAJES_LA_TORRIJA.Models
{
    public class CasaRural
    {
        [Key] 
        public int CasaRuralId { get; set; }

        public string Name { get; set; }

        //Inicio información dirección

        public string Street { get; set; }

        public int Numer { get; set; }

        public string Province { get; set; }

        [Display(Name = "Postal code")]
        public int PostalCode { get; set; }

        //Fin información dirección

        [Display(Name = "Guest capacity")]
        public int GuestCapacity { get; set; }

        [Display(Name = "Number of rooms")]
        public int NumberOfRooms { get; set; }

        [Display(Name = "Near activity")]
        public string[] NearActivity { get; set; }

        [Display(Name = "Pool")]
        public Boolean HasPool { get; set; }

        public string Description { get; set; }

        [Display(Name = "Parking")]
        public Boolean HasParking { get; set; }

    }
}
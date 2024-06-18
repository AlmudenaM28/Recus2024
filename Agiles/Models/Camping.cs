using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;


namespace _2023_VIAJES_LA_TORRIJA.Models
{
    public class Camping
    {
        [Key]
        public int CampingId { get; set; }

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

        [Display(Name = "Number of bungalows")]
        public int NumberOfBungalows { get; set; }

        [Display(Name = "Pool")]
        public Boolean HasPool { get; set; }

        public string Description { get; set; }
    }
}
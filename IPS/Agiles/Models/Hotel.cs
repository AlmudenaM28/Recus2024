using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace _2023_VIAJES_LA_TORRIJA.Models
{
    public class Hotel
    {
        [Key]
        public int HotelId { get; set; }

        public string Name { get; set; }

        //Inicio información dirección

        public string Street { get; set; }
        
        public int Numer { get; set; }

        public string Province { get; set; }

        [Display(Name = "Postal code")]
        public int PostalCode { get; set; }

        //Fin información dirección

        [Display(Name = "Number of stars")]
        public int Stars { get; set; }

        [Display(Name = "Rural")]
        public Boolean IsRural { get; set; }

        [Display(Name = "Mascots allowed")]
        public Boolean MascotsAllowed { get; set; }

        [Display(Name = "Adults only")]
        public Boolean IsAdultsOnly { get; set; }

        [Display(Name = "Thematic")]
        public Boolean IsThematic { get; set; }

        [Display(Name = "Parking")]
        public Boolean HasParking { get; set; }

        public string Description { get; set; }

        [Display(Name = "Number of rooms")]
        public int NumberOfRooms { get; set; }

        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }
    }
}
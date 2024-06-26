using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;


namespace _2023_VIAJES_LA_TORRIJA.Models
{
    public class Coche
    {
        [Key]
        public string CarId { get; set; }

        public string Company { get; set; }

        //Informacion direccion 
        public string Street { get; set; }

        public int Number { get; set; }

        public string Province { get; set; }

        [Display(Name = "Postal code")]
        public int PostalCode { get; set; }

        public string type { get; set; }

        [Display(Name = "Number of seats")]
        public int NumberSeats { get; set; }

        [Display(Name = "Number of cars of that type")]
        public int NumberCarsType { get; set; }

        [Display(Name = "Insurance")]
        public Boolean HasInsurance { get; set; }

        [Display(Name = "Child sit")]
        public string HasChildSit { get; set; }
        
        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}
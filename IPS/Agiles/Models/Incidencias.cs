using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace _2023_VIAJES_LA_TORRIJA.Models
{
    public class Incidencias
    {
        [Key]
        public string IncidenceId { get; set; }

        public string Type { get; set; }

        [Display(Name = "Company name")]
        public string CompanyName { get; set; }

        public string Description { get; set; }

        public int Importance { get; set; }
    }
}
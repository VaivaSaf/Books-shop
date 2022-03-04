using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AutoNuoma.Models
{
    public class ParduotaKnyga
    {
        [DisplayName("Kiekis")]
        [Required]
        public int kiekis { get; set; }
        [DisplayName("ISBN")]
        [Required]
        public string isbn { get; set; }
        public string id { get; set; }
        [DisplayName("Autorius")]
        public string autorius { get; set; }
        [DisplayName("Pavadinimas")]
        public string pavadinimas { get; set; }
        [DisplayName("Pardavejas")]
        public string pardavejas { get; set; }
        

    }
}
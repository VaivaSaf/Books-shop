using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AutoNuoma.Models
{
    public class Nauju_knygu_uzsakymas
    {
       [DisplayName("id")]
        public int id { get; set; }


       [DisplayName("knygos_isleidimo_metai_nuo")]
       [Required]
        public int knygos_isleidimo_metai_nuo { get; set; }


        [DisplayName("knygos_isleidimo_metai_iki")]
        [Required]
        public int knygos_isleidimo_metai_iki { get; set; }


        [DisplayName("kiekis")]
        [Required]
        public int kiekis { get; set; }


       [DisplayName("knygos_zanras")]
       [Required]
        public int knygos_zanras { get; set; }


        [DisplayName("kalba")]
        [Required]
        public int kalba { get; set; }


        [DisplayName("fk_Administratoriusprisijungimo_vardas")]
        public string fk_Administratoriusprisijungimo_vardas { get; set; }


        [DisplayName("fk_Pardavejasprisijungimo_vardas")]
        public string fk_Pardavejasprisijungimo_vardas { get; set; }


    }
}
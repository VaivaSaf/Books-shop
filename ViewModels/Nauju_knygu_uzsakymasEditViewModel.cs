using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AutoNuoma.ViewModels
{
    public class Nauju_knygu_uzsakymasEditViewModel
    {
        [DisplayName("id")]
        [MaxLength(10)]
        [Required]
        public int id { get; set; }
        [DisplayName("knygos_isleidimo_metai_nuo")]
        [MaxLength(10)]
        [Required]
        public int knygos_isleidimo_metai_nuo { get; set; }
        [DisplayName("knygos_isleidimo_metai_iki")]
        [MaxLength(10)]
        [Required]
        public int knygos_isleidimo_metai_iki { get; set; }
        [DisplayName("kiekis")]
        [Required]
        public int kiekis { get; set; }
        [DisplayName("kalba")]
        [Required]
        public int kalba { get; set; }
        [DisplayName("zanras")]
        [Required]
        public int zanras { get; set; }
       

        public IList<SelectListItem> kalbaList { get; set; }
        public IList<SelectListItem> zanrasList { get; set; }
    }
}
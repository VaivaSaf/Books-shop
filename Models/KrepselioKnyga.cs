using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AutoNuoma.Models
{
    public class KrepselioKnyga
    {
        public int kiekis { get; set; }
        public int id { get; set; }
        public string fk_KnygaISBN { get; set; }
        public int fk_Prekiu_krepselis { get; set; }
        public int fk_uzsakymas { get; set; }

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AutoNuoma.Models
{
    public class PrekiuKrepselis
    {
        public int id_Prekiu_krepselis { get; set; }
        public string fk_naudotojas { get; set; }
        public decimal suma { get; set; }
    }
}
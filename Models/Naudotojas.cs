using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AutoNuoma.Models
{
    public class Naudotojas
    {

        [DisplayName("Vardas")]
        [Required]
        public string vardas { get; set; }

        [DisplayName("Pavardė")]
        [Required]
        public string pavarde { get; set; }
        [DisplayName("Prisijungimo vardas")]
        [Required]
        public string prisijungimo_vardas { get; set; }
        [DisplayName("Elektroninis paštas")]
        [Required]
        public string el_pastas { get; set; }

        [DisplayName("Telefono numeris")]
        [Required]
        public int telefono_numeris { get; set; }

        [DisplayName("Gyvenamosios vietos adresas")]
        [Required]
        public string gyvenamosios_vietos_adresas { get; set; }
        [DisplayName("Gimimo data")]
        [Required]
        public DateTime gimimo_data { get; set; }
        [DisplayName("Lytis")]
        [Required]
        public int lytis { get; set; }
        [DisplayName("Slaptažodis")]
        [Required]
        public string slaptazodis { get; set; }
        public int role { get; set; }
    }
}
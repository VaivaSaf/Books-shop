﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AutoNuoma.Models
{
    public class Administratorius
    {
       
        [DisplayName ("prisijungimo_vardas")]
        [Required]
        public string prisijungimo_vardas { get; set; }
        
        [DisplayName("slaptazodis")]
        [Required]
        public string slaptazodis { get; set; }
    }
}
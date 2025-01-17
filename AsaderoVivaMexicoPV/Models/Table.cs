﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace AsaderoVivaMexicoPV.Models
{
    public class Table
    {
        [Key]
        public int TableId { get; set; }

        [Required(ErrorMessage = "Este es un campo requerido")]
        [Display(Name = "Mesa")]
        public int TableNumber { get; set; }

        public ICollection<Order> Orders { get; set; }

    }
}
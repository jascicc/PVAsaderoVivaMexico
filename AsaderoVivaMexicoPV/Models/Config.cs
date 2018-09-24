using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AsaderoVivaMexicoPV.Models
{
    public class Config
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Este es un campo requerido")]
        [Display(Name = "Dólar")]
        [DataType(DataType.Currency)]
        public float Dollar { get; set; }

        [Required(ErrorMessage = "Este es un campo requerido")]
        [Display(Name = "Nombre")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Este es un campo requerido")]
        [Display(Name = "RFC")]
        public string RFC { get; set; }

        [Required(ErrorMessage = "Este es un campo requerido")]
        [Display(Name = "Dirección")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Este es un campo requerido")]
        [Display(Name = "Teléfono")]
        [DataType(DataType.PhoneNumber)]
        public long Phone { get; set; }

    }
}
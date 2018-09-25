using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AsaderoVivaMexicoPV.Models
{
    public class Product
    {
        [Key]
        [Required]
        [Display(Name = "ID Producto")]
        public int ProductID { get; set; }

        [Required(ErrorMessage = "Este es un campo requerido")]
        [Display(Name = "Nombre Platillo")]
        [StringLength(50)]
        public string ProductName { get; set; }

        [Required(ErrorMessage = "Este es un campo requerido")]
        [Display(Name = "Categoría")]
        public int CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        public Category category { get; set; }

        [Required(ErrorMessage = "Este es un campo requerido")]
        [Display(Name = "Precio")]
        [DataType(DataType.Currency)]
        public float Price { get; set; }

        [Display(Name = "Imagen")]
        public byte[] Image { get; set; }

        public ICollection<Order> Orders { get; set; }

    }
}
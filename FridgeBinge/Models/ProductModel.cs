using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FridgeBinge.Models
{
    public class ProductModel
    {
        [DisplayName("Id Number")]
        public int Id { get; set; }

        [DisplayName("Product Name")]
        [Required]
        public string Name { get; set; }

        [DisplayName("Product Price")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than 0.")]
        [DataType(DataType.Currency)]
        [Required]
        public decimal Price { get; set; }

        [DisplayName("Info")]
        [Required]
        public string Description { get; set; }
    }
}

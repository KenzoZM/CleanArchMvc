using CleanArchMvc.Domain.Entitites;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchMvc.Application.DTOs
{
    public class ProductDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "The Name is required")]
        [MinLength(3)]
        [MaxLength(100)]
        [DisplayName("Name")]
        public string Name { get; private set; }

        [Required(ErrorMessage = "The Description is required")]
        [MinLength(5)]
        [MaxLength(200)]
        [DisplayName("Description")]
        public string Description { get; private set; }

        [Required(ErrorMessage = "The Price is required")]
        [Column(TypeName = "decimal(18,2)")]
        [DisplayFormat(DataFormatString = "{0:C2}")]
        [DisplayName("Description")]
        public decimal Price { get; private set; }

        [Required(ErrorMessage = "the stock is required")]
        [Range(1, 9999)]
        [DisplayName("Stock")]
        public int Stock { get; private set; }

        [MinLength(250)]
        [DisplayName("Product Image")]
        public string Image { get; private set; }

        [DisplayName("Categories")]
        public Category Category { get; set; }
        public int CategoryId { get; set; }
    }
}
